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
resource "kubernetes_service" "linkmanagerapi-clusterip-srvice" {
    metadata {
      name = "linkmanagerapi-clusterip-srvice"
    }
    spec {
      type = ClusterIP
      selector {
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
resource "kubernetes_service" "linkmanagerapinodeportservice-srv" {
    metadata {
      name = "linkmanagerapinodeportservice-srv"
    }
    spec {
      type = NodePort
      selector {
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
