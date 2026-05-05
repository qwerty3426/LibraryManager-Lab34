using System;

namespace BankSystem.Domain
{
    // 1. Клієнт
    public class Customer {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    // 2. Базовий аккаунт (SRP)
    public class Account {
        public int Id { get; set; }
        public string OwnerName { get; set; } = "";
        public decimal Balance { get; set; }
    }

    // 3. Ощадний рахунок (LSP - наслідування)
    public class SavingsAccount : Account {
        public decimal InterestRate { get; set; }
    }

    // 4. Транзакція
    public class Transaction {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    // 5. Відділення банку
    public class BankBranch {
        public int Id { get; set; }
        public string Address { get; set; } = "";
    }

    // 6. Банківська картка
    public class BankCard {
        public string CardNumber { get; set; } = "";
        public int AccountId { get; set; }
    }
}