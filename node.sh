docker service create \
 --name prod-node \
 --workdir="/home/node/app" \
 -e NODE_ENV="prod" \
 -e DB_SERVICE="prod-mongo.1.crqd45pu2kuiyahlqn4du8mrj" \
 --mount type=bind,source="/Users/oanadiana/Desktop/ArhitecturiOrientatePeServicii/backend/node",destination="/home/node/app" \
 -p 3000:3000 \
 --replicas 1 \
 --restart-condition on-failure \
 --restart-max-attempts 3 \
 --restart-window 20s \
 --network bridge \
 --health-interval 5s \
 --health-retries 2 \
 --health-timeout 2s \
 node:10.10.0-alpine /bin/sh -c "npm install -g nodemon && npm install && nodemon server/bookStoreServer.js | tee -a /home/node/app/log/$(date +%F.%R:%S)"
