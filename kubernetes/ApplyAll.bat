kubectl create secret generic db-secrets --from-literal=sa-password=p@@sw0rd
kubectl apply -f AllInOne.yaml