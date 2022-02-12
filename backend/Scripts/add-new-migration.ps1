param(
    [Parameter(Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$MigrationName
)

dotnet ef migrations add $MigrationName -p ..\ReservationSystem.DataAccess\ReservationSystem.DataAccess.csproj -o ..\ReservationSystem.DataAccess\Migrations -s ..\ReservationSystem\ReservationSystem.csproj