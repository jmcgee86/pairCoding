using System;
using System.Collections.Generic;

namespace pairCoding
{
    public class Transaction{
        public string type {get; set;}
        public DateTime transactionDate = DateTime.Now;
        public double amount {get; set;}

        public Transaction(double _amount, string _type){
            this.amount = _amount;
            this.type = _type;
        }
    }
    abstract class BankAccount
    {
        public string accountHolderName { get;  set; }
        public double accountBalance  { get;  set; }

        public int accountNumber {get;  set;}

        public DateTime openedOn = DateTime.Now;

        public virtual void ApplyTransaction()
        {
        }

    }

     class SavingsAccount : BankAccount{

         public double InterestRate {get; set; }

         public SavingsAccount()
         {
         }

         public SavingsAccount(string _accountHolderName, double _balance, double _interestRate)
         {
             this.accountHolderName = _accountHolderName;
             this.accountBalance = _balance;
             this.InterestRate = _interestRate;
         }

         public override void ApplyTransaction()
         {
            var amount = accountBalance*InterestRate;
            var type = "Add Interest";
            this.accountBalance += amount;
            var addInterest = new Transaction(amount, type );
            TransactionQueue.AddRemove(addInterest);
         }
     }

    class CheckingAccount : BankAccount
    {

        public int ServiceFee{get; set;}
        public  CheckingAccount(){

        }

        public CheckingAccount(string _accountHolderName, int _balance, int _serviceFee)
        {
            this.accountHolderName = _accountHolderName;
            this.accountBalance = _balance;
            this.ServiceFee = _serviceFee;
        }

        public override void ApplyTransaction()
        {
            var amount = this.ServiceFee;
            var type = "Service Fee";
            this.accountBalance -=amount;
            var addSerivceFee = new Transaction(amount, type);
            TransactionQueue.AddRemove(addSerivceFee);

        }
    } 

    public static class TransactionQueue
    {
        public static List<Transaction> Transactions = new List<Transaction>();

        public static void AddRemove (Transaction transaction)
        {
            Transactions.Add(transaction);
            Console.WriteLine("Enqueue new transaction");
        }
        public static Transaction AddRemove(string r)
        {
            if (r == "r" || r == "R")
            {
            var firstInLine = Transactions[0];
            Transactions.RemoveAt(0);
            Console.WriteLine("returning transaction of type: " + firstInLine.type + ", the amount of the transaction was: " + firstInLine.amount + " it was created on: " + firstInLine.transactionDate);
            return firstInLine;
            }
            else
                throw new Exception ("invalid request, please enter transaction or R to request transaction from Queue");
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            var myCheckingAccount = new CheckingAccount();
            myCheckingAccount.accountBalance = 1000;
            myCheckingAccount.accountHolderName = "Jim";
            myCheckingAccount.accountNumber = 1945;
            myCheckingAccount.ServiceFee = 5;


            var mySavingsAccount = new SavingsAccount("Jim", 5000, .09);

            mySavingsAccount.ApplyTransaction();
            Console.WriteLine("after interest add transaction: " + mySavingsAccount.accountBalance);

            myCheckingAccount.ApplyTransaction();
            Console.WriteLine("After fee transaction: " + myCheckingAccount.accountBalance);

            TransactionQueue.AddRemove("R");
            TransactionQueue.AddRemove("r");
        }
    }
}
