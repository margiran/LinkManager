resource "kubernetes_deployment" "linkmanager-deployment" {
    metadata {
      name= "linkmanager-deployment"
    }
    spec {
      replicas = 1
      selector {
          match_labels = {
              "app" = "linkmanagerservice"
          }
      }
      template {
          metadata {
              labels = {
                  "app" = "linkmanagerservice"
              }
          }
          spec {
              container {
                  name = "linkmanagerservice"
                  image = "margiran/linkmanagerapi:latest"
                  port {
                      container_port = 80
                  }
              }
          }
      }
    }   
}
resource "kubernetes_service" "linkmanagerapi-clusterip-service" {
    metadata {
      name = "linkmanagerapi-clusterip-service"
    }
    spec {
      type = "ClusterIP"
      selector = {
          app = "linkmanagerservice"
      }
      port {
        name = "linkmanagerservice"
        protocol = "TCP"
        port = 80
        target_port = 80
      }
    }
}
resource "kubernetes_service" "linkmanagerapi-loadbalancer-service" {
    metadata {
      name = "linkmanagerapi-loadbalancer"
    }
    spec {
      type = "LoadBalancer"
      selector = {
          app = "linkmanagerservice"
      }
      port {
        name = "linkmanagerservice"
        protocol = "TCP"
        port = 80
        target_port = 80
      }
    }
}
resource "kubernetes_persistent_volume_claim" "mssql-claim" {
  metadata {
    name = "mssql-claim"
  }
  spec {
    access_modes = ["ReadWriteMany"]
    resources {
      requests = {
        storage = "200Mi"
      }
    }
  }
}

