                                  [ External Users / Clients ]
                                                │
                                                ▼
                                 ┌──────────────────────────────┐
                                 │  Public IP (pip-apim-...)    │
                                 │  NSG (nsg-apim-...)          │
                                 └──────────────┬───────────────┘
                                                │
                                                ▼
                                 ┌──────────────────────────────┐
                                 │  Azure API Management (APIM) │
                                 │  (apim-cloudnative-platform) │
                                 └──────────────┬───────────────┘
                                                │
                                                ▼
┌────────────────────────────────────────────────────────────────────────────────────────────┐
│  Azure Virtual Network (aks-vnet-18487653) & NSG (aks-agentpool-18487653-nsg)              │
│                                                                                            │
│  ┌──────────────────────────────────────────────────────────────────────────────────────┐  │
│  │  Azure Kubernetes Service — AKS (aks-cloudnative-platform)                           │  │
│  │  Compute: VM Scale Set (aks-systempool-93979783-vmss) | LBs: Public & Internal       │  │
│  │                                                                                      │  │
│  │   ┌───────────────────────────┐  ┌───────────────────────────┐  ┌─────────────────┐  │  │
│  │   │      Orders Service       │  │     Inventory Service     │  │Analytics Service│  │  │
│  │   │          (.NET)           │  │          (.NET)           │  │    (Python)     │  │  │
│  │   │                           │  │                           │  │                 │  │  │
│  │   │  ID: cloudnative-orders-  │  │  ID: cloudnative-inventory│  │  ID: cloudnative│  │  │
│  │   │      identity             │  │      -identity            │  │  -analytics-id  │  │  │
│  │   └──────┬─────────────┬──────┘  └─────────────┬─────────────┘  └────────┬────────┘  │  │
│  └──────────┼─────────────┼───────────────────────┼─────────────────────────┼───────────┘  │
└─────────────┼─────────────┼───────────────────────┼─────────────────────────┼──────────────┘
              │             │                       │                         │
              │             └───────────┬───────────┘                         │
              │                         │                                     │
              ▼                         ▼                                     ▼
┌───────────────────────────┐ ┌───────────────────────────┐     ┌───────────────────────────┐
│   Azure Database for      │ │   Azure Managed Redis     │     │    Azure SQL Database     │
│   PostgreSQL Flexible     │ │   (cloudnative)           │     │ (CloudNativeAnalyticsDb)  │
│ (cloudnative-orders-pg-dev│ │                           │     │  Server: cloudnative-     │
│  & rg-cloudnative-...)    │ │  • Distributed Caching    │     │  sql-dev                  │
└───────────────────────────┘ └───────────────────────────┘     └───────────────────────────┘

──────────────────────────────────────────────────────────────────────────────────────────────
  Shared Platform, Messaging & Security Services (Central India)
──────────────────────────────────────────────────────────────────────────────────────────────
  ┌─────────────────────────┐  ┌─────────────────────────┐  ┌─────────────────────────────┐
  │   Azure Key Vault       │  │  Azure Storage Account  │  │  Azure Monitor / DCR        │
  │ (kv-cloudnative-platform│  │  (storagequeue123)      │  │ (MSCI-centralindia-aks-...) │
  │  + CSI Secrets Provider)│  │  • Async Queues / Blobs │  │  • Cluster Logs & Metrics   │
  └─────────────────────────┘  └─────────────────────────┘  └─────────────────────────────┘
s