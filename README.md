# Project Title

提供一個基本資料維護的 CRUD (Create,Read,Update,Delete) for C#.NET Core 實作範本。

## Features

- 提供開發三層式 (Presentation -> Application Logic -> Datasource) 架構的 CRUD 實作範本
- 同時提供 E.F Core (Entity Framework Core) 與 ADO.NET  資料存取方式，並可以切換資料庫存取的實作
- 實作 E.F Core 與 ADO.NET 的 DAO (Data Access Object) 均各自有撰寫單元測試程式 (Unit Test Code)
- 專案內嵌 Sqlite，並使用兩種實作方式：
  - File-based 為正常存取用的資料庫，提供給 View 操作
  - Memory-DB 應用在單元測試的 Create, Update, Delete ，避免影響到正常資料庫的存取行為

## Brief Description

網路上看到關於基本資料維護的 CRUD 範例幾乎都是資料導向 (data oriented) 的寫法，也就是把 MVC Controller 與 資料存取綁在一起。現實上稍有規模的專案採用這種開發模式會導致系統的複雜度提升，不容易應變。

花了幾天時間撰寫了 C#.NET Core (前年有寫過 Java Spring 的版本) 關於單一資料表的 CRUD 程式碼範本，這是一個完全符合三層式架構的應用程式，同時我這個專案範本可以切換 E.F Core 與 ADO.NET 資料庫的存取實作。

關於 Java/Spring 版本，我則打算再重新改寫，架構當然與 C#.NET Core 是一樣的，只是實作的細節必然會調整下。資料庫的存取方式則打算同時提供 JDBCTemplate 與 MyBatis 的 CRUD 範本。


## Development

- 使用 [Visual Studio Community 2022](https://visualstudio.microsoft.com/zh-hant/vs/community/) 版本。

- 使用 [C#.NET Core 6 LTS](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)

- 前端使用 [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) MVC Web 框架 (View by Razor)

- 後端資料庫使用 [Sqlite](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)，並同時使用 [E.F Core](https://docs.microsoft.com/en-us/ef/core/) 與 [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/) 兩種資料存取方式

## Class Diagram

<a href="https://raw.githubusercontent.com/kenming/images/main/uml/petstore-crud-3tier-class-diagram.png" target="_blank"><img src="https://raw.githubusercontent.com/kenming/images/main/uml/petstore-crud-3tier-class-diagram.png" width=1024px /></a>

## Build & Run

下載專案，使用 VS 2002 開啟 .sln 檔案，編輯「appsetting.json」檔案內關於 SqliteDB 檔案實際儲存路徑 (位於下載專案所在路徑內的 \Data 目錄)：

```JSON
  "ConnectionStrings": {
    "SqliteDB": "Data Source = C:\\Projects\\PetStoreCRUDSolution\\Data\\PetStoreDB.db",
    "SqliteMemoryDB": "Data Source = :memory:"
  }
```

<img src="https://raw.githubusercontent.com/kenming/images/main/screenshot/crud-project-appsetting.png" width=800px />

Sqlite 資料庫內已新增 Product Table 與 8 筆測試用資料，DDL & DML Script 已放於專案 \Data 目錄內。另外可以安裝如 [DBeaver Database Tool](https://dbeaver.io/) 連接 SQLite 並自行檢視與存取資料。

<img src="https://raw.githubusercontent.com/kenming/images/main/screenshot/sqlite-by-dbeaver.png" width=800px />


在「ManageProductService」類別內 Constructor 可以更改資料庫連結實作方式 (預設採用 E.F Core 6)

```C#
	public ManageProductService()
	{
		// default : use EF Core to connect to database.
		ProductDao = new ManageProductDaoByEFCore();
		//ProductDao = new ManageProductDaoByADO();
	}
```	

在 VS.NET 內執行「偵錯」→「啟動但不偵錯」或直接按下【CTRL-F5】執行，即可自動啟動瀏覽器進入首頁。點選「PetStore Demo」，即可出現關於 Product CRUD 的操作。

<img src="https://raw.githubusercontent.com/kenming/images/main/screenshot/petstore-crud-startup.png" width=800px />


## Unit Test (單元測試)

選單「Test」→ 「Test Explore」開啟「Test Explore (測試總管)」窗格，可以一次執行所有單元測試案例，或個別針對其中一個測試方法執行。

<img src="https://raw.githubusercontent.com/kenming/images/main/screenshot/crud-project-unit-test.png" height=480px />

如果要追蹤某一個單元測試執行情形 (先對某段程式碼內陳述設中斷點)，可以選擇「偵錯 (debug)」方式執行單元測試。

<img src="https://raw.githubusercontent.com/kenming/images/main/screenshot/crud-unit-test-debug.png" width=800px />

## Feedback

相關軟體思維問題，歡迎電郵或參與 Facebook - [軟體設計鮮思維](https://www.facebook.com/groups/softthinking) 社群討論。

## License

[MIT](LICENSE) © Richard Littauer