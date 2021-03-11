#!/bin/bash

DB_CONTAINER_NAME=epoll-database


# Start the container
if [ $# -eq 0 ]
    then
        docker run --rm -d --name $DB_CONTAINER_NAME \
            -e POSTGRES_DB=epoll \
            -e POSTGRES_USER=epoll \
            -e POSTGRES_PASSWORD=dev \
            -p 5432:5432 \
            postgres:13
    else
        docker run --rm -d --name $DB_CONTAINER_NAME \
            -e POSTGRES_DB=epoll \
            -e POSTGRES_USER=epoll \
            -e POSTGRES_PASSWORD=dev \
            -e PGDATA=/var/lib/postgresql/data/pgdata \
            -v $1:/var/lib/postgresql/data \
            -p 5432:5432 \
            postgres:13
fi

# Do migrations
dotnet ef database update
