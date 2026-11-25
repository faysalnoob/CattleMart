CATTLECART - FIXED PROJECT

চালানোর নিয়ম
১. ZIP-টি পুরোপুরি Extract করুন। ZIP-এর ভেতর থেকে সরাসরি EXE চালাবেন না।
২. Windows-এ .NET Framework 4.7.2 অথবা পরের 4.x এবং SQL Server Express LocalDB (MSSQLLocalDB) থাকতে হবে।
৩. Start_CattleCart.cmd চালান। প্রথমবার খালি Cattle_Cart_Pro ডেটাবেস ও প্রয়োজনীয় টেবিল তৈরি হবে।
৪. Sign up থেকে Farmer / Customer অ্যাকাউন্ট খুলুন। Admin অ্যাকাউন্ট তৈরি করতে Create_Admin.cmd চালান। কোনো ডিফল্ট পাসওয়ার্ড নেই।
৫. Farmer দিয়ে লগইন করে পশু আপলোড করুন। Customer দিয়ে লগইন করে পশুর তালিকা থেকে সারি নির্বাচন করে View / Buy করুন।

DATABASE CONFIGURATION
Default: (LocalDB)\MSSQLLocalDB, database Cattle_Cart_Pro, Windows authentication.
For another SQL Server, edit Run\CattleCart.exe.config. Source configuration is CattleCart\App.config.
An optional CATTLECART_CONNECTION_STRING environment variable overrides configuration.
The database account needs permission to create the database on first setup; alternatively ask your database administrator to create it first.
The original ZIP contains no database backup or SQL schema. This package supplies a schema built from the application's queries. Existing data from the original author's computer is not included.
The original hardcoded MSI\SQLEXPRESS server name has been removed.
Fresh database setup is tested. Existing external databases with different column types/lengths may need a separate migration. Existing plaintext passwords are accepted for compatibility; new accounts use salted PBKDF2 hashes.

PAYMENTS
This is an academic DEMO, not an integrated payment gateway. Use dummy card/mobile/PIN details only. No real money is charged and no payment credentials are stored. The demo delivery advance remains Tk 5000, matching the original project. A successful demo checkout saves one order and marks the selected animal unavailable. Actual animal price is stored separately. Do not use this app for real payments.

BUILD / EDIT
Open CattleCart.sln in Visual Studio with .NET desktop development and .NET Framework 4.7.2 targeting pack.
Or run powershell -ExecutionPolicy Bypass -File .\Build.ps1
Build.ps1 refreshes the Run folder from the rebuilt executable.
No third-party NuGet packages are required.

TESTS
Run powershell -ExecutionPolicy Bypass -File .\tests\Run-Tests.ps1
Requires Visual Studio build tools and LocalDB. Tests create an isolated uniquely named database and delete that test database afterward. They do not modify the normal application database.
See TEST_RESULTS.txt for recorded validation and FIXES.txt for changes.
