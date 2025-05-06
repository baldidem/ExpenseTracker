# ExpenseTracker

ExpenseTracker, saha personelinin masraf takibini ve onay süreçlerini kolaylaştırmak için geliştirilmiş bir ASP.NET Core Web API projesidir. Proje, Clean Architecture prensiplerine uygun olarak yapılandırılmıştır ve JWT tabanlı kimlik doğrulama ile güvenliği sağlanır.

📂 Proje Yapısı

ExpenseTracker/
├── ExpenseTracker.API
├── ExpenseTracker.Application
├── ExpenseTracker.Domain
├── ExpenseTracker.Infrastructure
├── ExpenseTracker.Persistence
├── ExpenseTracker.sln

API: Controller'lar, Middlewares, Program.cs

Application: Service Interface'ler, DTO'lar, Validatörler, AutoMapper Profilleri

Domain: Entity tanımları

Infrastructure/Persistence: EF Core + Dapper + Repository & UnitOfWork

🚀 Kurulum & Çalıştırma

Projeyi Klonlayın:

git clone <repository-url>

Veritabanı Ayarları:

appsettings.json dosyasındaki ConnectionStrings kısmını düzenleyin.

Migration oluşturun & veritabanını güncelleyin:

dotnet ef migrations add InitialCreate
dotnet ef database update

Projeyi Başlatın:

dotnet run --project ExpenseTracker.API

Swagger UI: https://localhost:<port>/swagger
🗄️ Seed Data Bilgisi

Projede varsayılan olarak iki kullanıcı seed edilmiştir:

Admin Kullanıcı:

Email: admin@gmail.com

Password : Admin123

Rol: Admin

Staff (Personnel) Kullanıcı:

Email: staff@gmail.com

Rol: Staff

Password: Staff123

Bu kullanıcılar ile doğrudan giriş yaparak API'yi test edebilirsiniz.

🔐 Kimlik Doğrulama & Yetkilendirme

JWT Bearer Token kullanılır.

AuthController üzerinden token alınır.

API uçları Admin ve Personnel rollerine göre yetkilendirilmiştir.

⚙️ Kullanılan Teknolojiler

ASP.NET Core 7 Web API

Entity Framework Core

Dapper

JWT Authentication

AutoMapper

FluentValidation

Swagger / OpenAPI

📊 Raporlama

Dapper ile hızlı sorgular ve özelleştirilebilir raporlar:

Expense Approval Status Report: Onaylı ve reddedilmiş masrafların dökümü. (Staff)

User Payment Report: Kullanıcı bazlı ödeme geçmişi. (Admin)

Expense Report: Kullanıcının masraf gecmişi.(Admin)

Company Payment Rate Report: Verilen tarihler arası yapılan ödeme geçmişi. (Admin)

👤 Rollere Göre Yetkilendirme

Admin:

Kullanıcı ve rol yönetimi

Masraf onay/red işlemleri

Rapor görüntüleme

Personnel:

Masraf oluşturma & listeleme

🚦 API Uç Noktaları

🔑 AuthController

POST /api/Auth/Login: Kullanıcı girişi ve JWT token alımı.

Request Body Örneği:

{
  "email": "admin@gmail.com",
  "password": "YourPassword123"
}

👥 UsersController

POST /api/Users: Yeni kullanıcı oluşturma (Admin)

Request Body Örneği:

{
  "name": "John",
  "surname": "Doe",
  "email": "john.doe@example.com",
  "password": "Password123",
  "roleId": 2,
  "iban": "TR000000000000000000000000"
}

PUT /api/Users/{id}: Kullanıcı güncelleme (Admin)

Request Body Örneği:

{
  "name": "John Updated",
  "surname": "Doe Updated",
  "email": "john.updated@example.com",
  "roleId": 2,
  "iban": "TR000000000000000000000001"
}

🛡️ RolesController

POST /api/Roles: Yeni rol oluşturma (Admin)

Request Body Örneği:

{
  "name": "Manager"
}

💼 ExpenseCategoriesController

POST /api/ExpenseCategories: Yeni masraf kategorisi oluşturma (Admin)

Request Body Örneği:

{
  "name": "Travel"
}

💸 ExpensesController

POST /api/Expenses: Yeni masraf ekleme (Personnel)

Request Body Örneği:

{
  "amount": 1500.75,
  "currency": 1,
  "expenseCategoryId": 3,
  "description": "Flight ticket to conference"
}

📈 ReportsController

GET /api/Reports/ExpenseApprovalStatus: Onaylı/Reddedilmiş masrafların raporu.

Query Params: startDate, endDate, status

GET /api/Reports/UserPaymentRate: Kullanıcının ödeme geçmiş raporu.

Query Params: startDate, endDate, userId

🛠️ Öne Çıkan Özellikler

✅ Clean Architecture & Katmanlı Mimari

✅ Generic Repository + Unit of Work

✅ Global Exception Handling Middleware

✅ DTO & FluentValidation

✅ Role-based Authorization

✅ Dapper ile performanslı raporlama

📂 Önemli Dosyalar

Program.cs: Middleware ve DI konfigürasyonu

Controllers/: API uç noktaları

Middlewares/ExceptionMiddleware.cs: Global hata yönetimi

Application/Validators/: FluentValidation sınıfları

