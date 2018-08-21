using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WzorceLib.Czynnosciowe
{
    public class Bank
    {
        private readonly List<BankAccount> _accounts;
        private const double InterestRate = 0.07;

        public Bank()
        {
            //Fake init
            _accounts = new List<BankAccount>()
            {
                new BankAccount("WD40"),
                new BankAccount("AD80"),
                new BankAccount("WW99"),
                new BankAccount("UU88"),
            };

            _accounts.ForEach( a => a.Add(100));

        }

        public bool Transfer(string fromID, string toID, double amount)
        {
            var from = FindAccount(fromID);
            var to = FindAccount(toID);

            return from.ExecuteOperation(new TransferToOperaion(to, amount));
        }

        public bool AddInterests(string accountID)
        {
            var account = FindAccount(accountID);
            return account.ExecuteOperation(new AddInterest(InterestRate));
        }

        public bool ChargeAccount(string accountID)
        {
            var account = FindAccount(accountID);
            return account.ExecuteOperation(new ChargeAccount(100));
        }

        public string GetAccountHistory(string accountID)
        {
            var account = FindAccount(accountID);
            return account.PrintAccountHistory();
        }

        private BankAccount FindAccount(string accountID)
        {
            return _accounts
                .Single(a => a.Id.Equals(accountID, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class BankAccount
    {
        public double Balance { get; private set; }
        public string Id { get; private set; }
        private readonly Stack<ICommand> _accountHistory;

        public BankAccount(string id)
        {
            Id = id;
            _accountHistory = new Stack<ICommand>();
        }

        public bool ExecuteOperation(ICommand command)
        {
            var result = command.Execute(this);
            LogAccountHistory(command);
            return result;
        }

        public string PrintAccountHistory()
        {
            var stringBuilder = new StringBuilder();

            foreach (var command in _accountHistory)
            {
                stringBuilder.Append(command);
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        public bool Add(double amount)
        {
            Balance += amount;
            return true;
        }

        public bool Substract(double amount, bool debtPossible)
        {
            if (!debtPossible && Balance - amount < 0)
            {
                return false;
            }

            Balance -= amount;
            return true;
        }

        private void LogAccountHistory(ICommand command)
        {
            _accountHistory.Push(command);
        }

    }

    // Command będzie w odpowiedni sposob zarzadzalo poleceniami które będą kombinacja Add i Substract
    // Kazda z Command będize przeładowywać toString tak by łatwo dało się wyświetlić historię
    public interface ICommand
    {
        bool Execute(BankAccount bankAccount);
    }

    public class TransferToOperaion : ICommand
    {
        private readonly BankAccount _to;
        private readonly double _amount;

        public TransferToOperaion(BankAccount to, double amount)
        {
            _to = to;
            _amount = amount;
        }

        public bool Execute(BankAccount from)
        {
            if (from.Substract(_amount, false))
            {
                if (_to.Add(_amount))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("Polecenie przekazu na konto : {0} w wysokości {1}", _to.Id, _amount);
        }
    }

    public class AddInterest : ICommand
    {
        private readonly double _interestRate;
        private double _interestAdded;
        public AddInterest(double interestRate)
        {
            _interestRate = interestRate;
        }

        public bool Execute(BankAccount bankAccount)
        {
            _interestAdded = bankAccount.Balance * _interestRate;
            return bankAccount.Add(_interestAdded);
        }

        public override string ToString()
        {
            return string.Format("Doliczenie odsetek w wysokosci {0}", _interestAdded);
        }

    }

    public class ChargeAccount : ICommand
    {
        private readonly double _amount;

        public ChargeAccount(double amount)
        {
            _amount = amount;
        }

        public bool Execute(BankAccount bankAccount)
        {
            return bankAccount.Substract(_amount, true);
        }

        public override string ToString()
        {
            return string.Format("Obciążenie konta przez bank w wysokości {0}", _amount);
        }
    }

    public class Polecenie : IExampleRunnable
    {
        public void Run()
        {
            var HSBC = new Bank();

    /*          Available Accounts
     *          new BankAccount("WD40"),
                new BankAccount("AD80"),
                new BankAccount("WW99"),
                new BankAccount("UU88"),
     */

            HSBC.AddInterests("WD40");
            HSBC.ChargeAccount("WD40");
            HSBC.Transfer("WD40", "AD80", 50);
            HSBC.Transfer("WD40", "UU88", 20);
            Console.WriteLine(HSBC.GetAccountHistory("WD40"));
        }
    }
}
