Сервис павтоматизации роверки пакетов в сервисах

Для запускаg проекта необходимо выполнить команду `PackageVersionValidator.dll <package name> <package version>`

Где <package name> - название библиотеки, <package version> - версия библиотеки

Пример: `PackageVersionValidator.dll Test.Package 1.0.0`

В appsettings.json необходмо указать:
* в поле BaseUrl значением url сервиса. Например http://localhost:5000
* в поле Services списком передать пути до сервисов. Например ["/v1/test/api",  "/v2/test/api"]

