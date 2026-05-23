# BankSystem — Release Candidate v1.0.0

BankSystem — консольна банківська система для роботи з базовими операціями над рахунками.

## Про проєкт

Проєкт демонструє багатошарову архітектуру з чітким розділенням домену, бізнес-логіки, сховища та UI. Його мета — показати готову структуру для подальшого розширення релізу.

## Архітектура

- `src/BankSystem.Domain` — доменні моделі та інтерфейси.
- `src/BankSystem.Application` — бізнес-логіка.
- `src/BankSystem.Infrastructure` — реалізації сховища.
- `src/BankSystem.Console` — користувацький інтерфейс.
- `tests/BankSystem.Tests` — набір unit-тестів.

## Реалізовано

- створення рахунків;
- депозит;
- зняття коштів;
- переказ між рахунками;
- in-memory репозиторій;
- unit-тести;
- CI-процес.

## Структура проекту

- `src/` — програмний код;
- `tests/` — тести;
- `docs/` — документація;
- `.github/` — CI workflow;
- `CHANGELOG.md` — історія змін;
- `USER_GUIDE.md` — користувацький гайд;
- `DEVELOPER_GUIDE.md` — гайд для розробників.

## Запуск

### Збірка

```bash
dotnet build
```

### Запуск

```bash
dotnet run --project src/BankSystem.Console
```

### Тести

```bash
dotnet test
```

## Документація

- `USER_GUIDE.md`
- `DEVELOPER_GUIDE.md`
- `CHANGELOG.md`
- `docs/release-plan.md`
- `docs/defense-qa.md`
- `docs/syllabus-coverage.md`
- `docs/iteration-1.md`
- `docs/iteration-2.md`
- `docs/iteration-3.md`
- `docs/DEMO.md`

## Статус

Release Candidate v1.0.0

Проєкт готовий до подальшого тестування та презентації релізу.

