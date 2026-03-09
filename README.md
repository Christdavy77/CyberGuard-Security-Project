# CyberGuard - Dashboard de Gestion des Vulnérabilités

## Aperçu du Projet
CyberGuard est une solution full-stack permettant de centraliser et de suivre les failles de sécurité. L'objectif était de construire une architecture robuste capable de gérer le cycle de vie d'une vulnérabilité, de sa détection à son archivage.

## Stack Technique
* **Backend :** .NET 10 Web API
* **Persistance :** SQL Server (Docker Container)
* **ORM :** Entity Framework Core
* **Architecture :** Clean Architecture
* **Frontend :** Interface Dashboard (JS Fetch API / Bootstrap)

## Installation et Setup

### 1. Base de données (Docker)
Lancement de l'instance SQL Server avec Docker :
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourPassword123!" -p 1433:1433 --name sql_server -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)

2. Backend (API)
Restauration des dépendances et lancement du serveur :

Bash
dotnet restore
dotnet run --project CyberGuard.API


3. Frontend (Dashboard)
Le Dashboard est accessible via le fichier index.html. Assurez-vous que l'API tourne sur le port 5247.

Implémentation technique
Clean Architecture : Séparation stricte de la logique métier (Domain) et de la persistance (Infrastructure).

CORS Policy : Configuration d'une politique d'accès sécurisée pour les requêtes cross-origin.

Logging : Suivi des événements de sécurité et des erreurs directement dans la console serveur.