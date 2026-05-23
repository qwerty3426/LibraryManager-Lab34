# Defense Questions

## Why layered architecture?
Layered architecture separates domain logic, application services, persistence, and user interface. Це забезпечує простіший розвиток та тестування.

## Why use Repository Pattern?
Repository Pattern ізолює доступ до даних та дозволяє легко замінити сховище без зміни бізнес-логіки.

## Why use a domain model?
Domain model (`Account`) зберігає бізнес-дані окремо від UI та сервісів.

## What is the current release status?
Проєкт є Release Candidate v1.0.0 з базовою консольною реалізацією та документацією.

## What подальші покращення плануються?
- додати JSON persistence;
- додати інтеграційні тести;
- покращити UI;
- додати quality gate у CI.
