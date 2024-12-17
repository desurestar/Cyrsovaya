#!/bin/bash

# Проверка на наличие аргументов
if [ "$#" -ne 2 ]; then
  echo "Использование: $0 <путь к каталогу> <маска>"
  exit 1
fi

# Аргументы
SOURCE_DIR=$1
MASK=$2

# Проверка, существует ли исходный каталог
if [ ! -d "$SOURCE_DIR" ]; then
  echo "Ошибка: Каталог '$SOURCE_DIR' не существует."
  exit 1
fi

# Создание подкаталога в /tmp с именем, содержащим текущее время
TIMESTAMP=$(date +"%Y%m%d_%H%M%S")
TARGET_DIR="/tmp/$TIMESTAMP"
mkdir -p "$TARGET_DIR"

# Копирование файлов по маске
find "$SOURCE_DIR" -maxdepth 1 -type f -name "$MASK" -exec cp {} "$TARGET_DIR" \;

# Проверка, были ли скопированы файлы
if [ "$(ls -A "$TARGET_DIR")" ]; then
  echo "Файлы успешно скопированы в каталог: $TARGET_DIR"
else
  echo "Не найдено файлов, соответствующих маске '$MASK' в каталоге '$SOURCE_DIR'."
  rmdir "$TARGET_DIR"  # Удаляем пустой каталог
fi
