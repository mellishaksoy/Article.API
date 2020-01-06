# Article.API

## Uygulamanın Çalıştırılması Hakkında

- Article.API 'ın çalışması için Authentication için IdentityServer4 Projesinin de çalışıyor olması gerekmektedir. 
- Ayarlar AppSettings dosyasından yönetilmektedir.
- IdentityServer4 ve Article.API uygulamaları veritabanı olarak MSSql ile çalışmaktadır.
- Uygulamalar EFCore kullanılarak CodeFirst olarak geliştirilmiştir. VeriTabanı uygulamalar ayağa kalkarken migrate edildiğinden
  sql scriptine gerek duyulmamıştır.
- Bazı entity'ler için context boş ise data seed işlemi yapılmaktadır.
- Tablolarda SoftDelete kullanılmıştır.
- 
Örnek: User: 1- Username = "admin",
                Password = "admin123"
				
			 2- Username = "guest",
                Password = "qwe123"
				
			 3- Username = "melis",
                Password = "Password123!"
				
	Client:  ClientId = "Article.API.Client",
			 Secret = "secret"
			 Scope = Article.API
	Category 
             1- Id = new Guid("7dca9794-bfdb-41c2-bac6-5b4371ac6c26"),
                Name = "Education"
               
             2- Id = new Guid("65d6f702-6a72-4814-b40a-7f4f72f9471d"),
                Name = "Novel"
               
             3- Id = new Guid("a90fd3f2-ce26-4417-a78b-75b7bc98bbe9"),
                Name = "Adventure"
               
	Tag
			1- Id = new Guid("3d343607-0112-488a-92a8-37ddc99366f1"),
               Name = "Funny"
			2- Id = new Guid("a58197d8-0922-4e2e-9268-e4f98ec0bad1"),
               Name = "Novel"
			3- Id = new Guid("a90fd3f2-ce26-4417-a78b-75b7bc98bbe9"),
               Name = "Fluent"
			 
## POSTMAN KODU
			 
Identity Token Alma Postman kodu
---------------------------------
curl -X POST \
  http://localhost:62940/connect/token \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'Postman-Token: b170a090-65cb-4ec1-93a5-fd30a8c7e61d' \
  -H 'cache-control: no-cache' \
  -d 'grant_type=password&client_id=Article.API.Client&client_secret=secret&username=melis&password=Password123!&scope=Article.API&undefined='
	
---------------------------------

Create Article
---------------------------------
curl -X POST \
  https://localhost:44341/api/v1/articles \
  -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjJkMjgxZGYxOWQxZmExMTYzMjE5Y2E3OTVmNjNiOTRlIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NzgzMDE0NzIsImV4cCI6MTU3ODMwNTA3MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo2Mjk0MCIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjYyOTQwL3Jlc291cmNlcyIsIkFydGljbGUuQVBJIl0sImNsaWVudF9pZCI6IkFydGljbGUuQVBJLkNsaWVudCIsInN1YiI6ImU3YjQ1YTE0LWQ4NWItNGE2ZC04YTA3LTViMzQ1NTkxYjAyYSIsImF1dGhfdGltZSI6MTU3ODMwMTQ2MywiaWRwIjoibG9jYWwiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Im1lbGlzIiwibmFtZSI6Ik1lbGlzIiwid2Vic2l0ZSI6Imh0dHBzOi8vbWVsaXMuY29tIiwicm9sZSI6IkFkbWluIiwic2NvcGUiOlsiQXJ0aWNsZS5BUEkiXSwiYW1yIjpbInB3ZCJdfQ.OMwq9CpuN7beJnsaHmfCEHJswUMZWIciaHbYyyabWFWY6Uf_oSSuXStBhm8EfOdRfpG9oOO8Uc2dvDDHLpsfMeAkhf8HV664CPVvoao-YwMh3fZunatPVu44Gi0GNC_CgMt-44Xmt52hot2CtQ-JY5lPTfzqrUBbF3svlsaQ5ehPJAFyAOc8HSjXolFc1v6IwpG7wo-9Vy-3oZv_fHP8GgjxumyDa3KBaXhCNgcrJgZj47u6ptg1P3ugoF6XUwZdtaeY-dRhGgtE02AGMLX65aDdSXQ5KjGOTvDH9PPXqErpFuZ4u1rCuemT2wlzgr3l0dYD6gY2hXrefjUFxKgoPQ' \
  -H 'Content-Type: application/json' \
  -H 'Postman-Token: 17f8405f-5293-4067-9e48-32b108234642' \
  -H 'cache-control: no-cache' \
  -d '{
	"Title":"Makale Örneği 2",
	"Body":"Identity Server 4 ile authentication sağlayarak, code first kullanarak, SeriLog ile Request Middleware yapısı ile loglama sağlanarak makale paylaşımı ve blog mantığında geliştirme yapılmıştır.",
	"CategoryId":"7DCA9794-BFDB-41C2-BAC6-5B4371AC6C26",
	"TagIds":["a90fd3f2-ce26-4417-a78b-75b7bc98bbe9"]
	
}'

Create Article
---------------------------------
curl -X POST \
  https://localhost:44341/api/v1/articles \
  -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjJkMjgxZGYxOWQxZmExMTYzMjE5Y2E3OTVmNjNiOTRlIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NzgzMDE0NzIsImV4cCI6MTU3ODMwNTA3MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo2Mjk0MCIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjYyOTQwL3Jlc291cmNlcyIsIkFydGljbGUuQVBJIl0sImNsaWVudF9pZCI6IkFydGljbGUuQVBJLkNsaWVudCIsInN1YiI6ImU3YjQ1YTE0LWQ4NWItNGE2ZC04YTA3LTViMzQ1NTkxYjAyYSIsImF1dGhfdGltZSI6MTU3ODMwMTQ2MywiaWRwIjoibG9jYWwiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Im1lbGlzIiwibmFtZSI6Ik1lbGlzIiwid2Vic2l0ZSI6Imh0dHBzOi8vbWVsaXMuY29tIiwicm9sZSI6IkFkbWluIiwic2NvcGUiOlsiQXJ0aWNsZS5BUEkiXSwiYW1yIjpbInB3ZCJdfQ.OMwq9CpuN7beJnsaHmfCEHJswUMZWIciaHbYyyabWFWY6Uf_oSSuXStBhm8EfOdRfpG9oOO8Uc2dvDDHLpsfMeAkhf8HV664CPVvoao-YwMh3fZunatPVu44Gi0GNC_CgMt-44Xmt52hot2CtQ-JY5lPTfzqrUBbF3svlsaQ5ehPJAFyAOc8HSjXolFc1v6IwpG7wo-9Vy-3oZv_fHP8GgjxumyDa3KBaXhCNgcrJgZj47u6ptg1P3ugoF6XUwZdtaeY-dRhGgtE02AGMLX65aDdSXQ5KjGOTvDH9PPXqErpFuZ4u1rCuemT2wlzgr3l0dYD6gY2hXrefjUFxKgoPQ' \
  -H 'Content-Type: application/json' \
  -H 'Postman-Token: 001b4979-c508-49be-b292-42bad40f7697' \
  -H 'cache-control: no-cache' \
  -d '{
	"Title":"Makale Örneği 2",
	"Body":"Identity Server 4 ile authentication sağlayarak, code first kullanarak, SeriLog ile Request Middleware yapısı ile loglama sağlanarak makale paylaşımı ve blog mantığında geliştirme yapılmıştır.",
	"CategoryId":"7DCA9794-BFDB-41C2-BAC6-5B4371AC6C26",
	"TagIds":["a90fd3f2-ce26-4417-a78b-75b7bc98bbe9","A58197D8-0922-4E2E-9268-E4F98EC0BAD1"]
	
}'

-----------------------
Filter and Search Articles
--------------------------

curl -X GET \
  'https://localhost:44341/api/v1/Articles?Title=Makale&CategoryName=Education&Tag=Novel' \
  -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjJkMjgxZGYxOWQxZmExMTYzMjE5Y2E3OTVmNjNiOTRlIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NzgzMDE0NzIsImV4cCI6MTU3ODMwNTA3MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo2Mjk0MCIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjYyOTQwL3Jlc291cmNlcyIsIkFydGljbGUuQVBJIl0sImNsaWVudF9pZCI6IkFydGljbGUuQVBJLkNsaWVudCIsInN1YiI6ImU3YjQ1YTE0LWQ4NWItNGE2ZC04YTA3LTViMzQ1NTkxYjAyYSIsImF1dGhfdGltZSI6MTU3ODMwMTQ2MywiaWRwIjoibG9jYWwiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Im1lbGlzIiwibmFtZSI6Ik1lbGlzIiwid2Vic2l0ZSI6Imh0dHBzOi8vbWVsaXMuY29tIiwicm9sZSI6IkFkbWluIiwic2NvcGUiOlsiQXJ0aWNsZS5BUEkiXSwiYW1yIjpbInB3ZCJdfQ.OMwq9CpuN7beJnsaHmfCEHJswUMZWIciaHbYyyabWFWY6Uf_oSSuXStBhm8EfOdRfpG9oOO8Uc2dvDDHLpsfMeAkhf8HV664CPVvoao-YwMh3fZunatPVu44Gi0GNC_CgMt-44Xmt52hot2CtQ-JY5lPTfzqrUBbF3svlsaQ5ehPJAFyAOc8HSjXolFc1v6IwpG7wo-9Vy-3oZv_fHP8GgjxumyDa3KBaXhCNgcrJgZj47u6ptg1P3ugoF6XUwZdtaeY-dRhGgtE02AGMLX65aDdSXQ5KjGOTvDH9PPXqErpFuZ4u1rCuemT2wlzgr3l0dYD6gY2hXrefjUFxKgoPQ' \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'Postman-Token: 1800e8ec-f498-45eb-b9e9-ff5f52fcb82b' \
  -H 'cache-control: no-cache'


## Kullanılan Patternler

- Repository Pattern 
- MVC


## Projede Kullanılan Teknolojiler Hakkında Tecrübelerim Olanlar

- Microservice mimarisi, dependency injection, repository pattern
- EF Core
- SeriLog
- Authentication - IdentityServer4
- Global Error Handling
- Restful API


## Daha Geniş Vaktim Olsaydı

- Swagger ekleyebilirdim.
- UnitTest yazabilirdim.
- Vue.js kullanarak UI üzerinden de gösterim yapmak isterdim.
- 1 günde yaptığım için bazı dosyaları direk yazıp geçtim, ClassLibrary haline getirebilirdim.
- Authentication işlemini Identity ile çözdüğüm için Cache mekanizmasını kullanırken Identity'nin açtığı sessionlar üzerinde gittim.
Authentication işleminde SAML ya da kendi user db yapımızı tasarlayarak da geliştirebilirdim.
- VeriTabanı tasarımını bir blog tasarımından aldım. Daha detaylı bir tasarım seçebilirdim.
- Policy mekanizması ile kullanıcıların erişimlerini sınırlandırabilirdim. Örneğin makaleyi yalnızca ekleyen ya da admin silebilir gibi.
- Validasyonlar şimdilik if conditional yapısı ile sağlanmıştır. Validasyon için FluentValidation kullanılabilirdi.
- SeriLog kullanılırken Request Logger Middleware'i kullanılmıştır. Her yere Log yazarken kritik olma durumuna dikkat edilerek yazılabilirdi hatalar veya uyarılar.
- Exception Handlinte hata açıklamalı multi language olabilirdi.
- Makale üzerinden arama yaparken string alanlarının multi language olması yapılabilirdi.

## NOT 
- Projede SoftDelete, DeleteAuditing ve InsertAuditing kullanılmıştır.
- Postman Requestlerinde Bearer Token istenmektedir. Yoksa 401 Unauthorized hatası alınmaktadır.
- Post requestleri ile create yapıldığında response header'ında yaratılan entity'nin Idsi dönülmektedir. Bu yüzden Get requesti yapılmalıdır.
- Listelerken Page yapısı kullanılmıştır. 
