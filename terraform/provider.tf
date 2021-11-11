terraform {
    # required_version = "~> 0.13"
    required_providers {
      mycloud = {
          source = "hashicorp/kubernetes"
          version = "~> 1.13"
      }
    }
    backend "local" {
        path = "/terraform/terraform.tfstate"
    }
}

provider "kubernetes" {
    host= "https://kubernetes.docker.internal:6443"
}


