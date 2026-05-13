# Матриця тестування

| Сценарій | Тест |
|---|---|
| Створення акаунта | CreateAccount_ShouldAddAccount |
| Зняття коштів | Withdraw_ShouldDecreaseBalance |
| Недостатньо коштів | Withdraw_ShouldThrowException |
| Фільтрація акаунтів | GetRichAccounts_ShouldReturnFiltered |
| Збереження JSON | Save_ShouldCreateJsonFile |
| Завантаження JSON | Load_ShouldReadAccounts |
| Пошкоджений JSON | Load_ShouldHandleBrokenJson |
| Standard комісія | StandardFee_ShouldCalculateCorrectly |
| Premium комісія | PremiumFee_ShouldCalculateCorrectly |
| Відсутній акаунт | 
Withdraw_ShouldThrow_WhenAccountMissing |