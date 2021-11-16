kubectl create secret generic mssql --from-literal=SA_PASSWORD="p@@sw0rd"
kubectl apply -f AllInOne.yaml