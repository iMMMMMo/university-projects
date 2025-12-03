# Sports Clothes Manager - C# / ASP.NET / WPF

Projekt przedstawia prosty system do zarządzania producentami i produktami z kategorii odzieży sportowej.  
Aplikacja została zrealizowana jako projekt studencki i demonstruje wielowarstwową architekturę z wykorzystaniem **C#**, **ASP.NET Core**, **Entity Framework Core**, **SQLite** oraz **WPF**.

## Zawartość projektu

- **CORE** - modele oraz podstawowa logika domenowa  
- **INTERFACES** - interfejsy DAO  
- **DAOMOCK** - implementacja testowa (mock)  
- **DAOSQL** - implementacja dostępu do danych poprzez SQLite  
- **UI** - aplikacja desktopowa WPF  
- **UI.WEBAPP** - aplikacja webowa ASP.NET Core  


## Jak uruchomić

###  Opcja 1: Aplikacja Web (ASP.NET Core)
1. Otwórz rozwiązanie w Visual Studio. 
2. Ustaw projekt **SportsClothes.UI.WEBAPP** jako startowy.  
3. Uruchom aplikację (`F5` lub `dotnet run`).  

###  Opcja 2: Aplikacja Desktopowa (WPF)
1. Otwórz rozwiązanie w Visual Studio.  
2. Ustaw projekt **SportsClothes.UI** jako startowy.  
3. Uruchom aplikację WPF (`F5`).  

Obie aplikacje korzystają z tej samej warstwy danych.

## Baza danych

System korzysta z pliku SQLite:

```
SportsClothes.DAOSQL/Database/DaoSqlite.db
```

Plik znajduje się w repozytorium i aplikacja nie wymaga generowania migracji.

## Wymagania

- .NET 8  
- Visual Studio 
- SQLite  
