docker service create \
 --name prod-mongo \
 --workdir="/home/node/app" \
 --mount type=bind,source="/Users/oanadiana/Desktop/ArhitecturiOrientatePeServicii/backend/db",destination="/data/db" \
 --replicas 1 \
 --restart-condition on-failure \
 --restart-max-attempts 3 \
 --restart-window 20s \
 --health-interval 5s \
 --health-retries 2 \
 --health-timeout 2s \
mongo:4.0.2-xenial
