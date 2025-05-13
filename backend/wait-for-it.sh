#!/bin/sh

HOST=$(echo "$1" | cut -d ':' -f 1)
PORT=$(echo "$1" | cut -d ':' -f 2)
shift

# Remove o separador '--' se presente
if [ "$1" = "--" ]; then
  shift
fi

echo "Esperando por $HOST:$PORT..."
while ! nc -z $HOST $PORT; do
  sleep 1
done

echo "$HOST:$PORT está disponível. Executando: $@"
exec "$@"
