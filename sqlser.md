docker pull mcr.microsoft.com/mssql/server

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Senha123!" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   b283d9eef254
   
Modelo para conexao com container
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ClientesDB;Trusted_Connection=True;TrustServerCertificate=True;User ID=sa;Password='Senha123!';integrated security=false;"
  },