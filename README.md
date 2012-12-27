7 Semester - CSaN (Computing Systems and Networks) (VKSiS)
==========================================================

Лаба 1.
Написать прогу, которая реализует функцию печатной машинки.
В первом окне набираем текст, во втором отображаем тект... (и наоборот).
Должны присутствовать средства выбора COM порта.
_______________________________________________________________________

Лаба 2. Байт стаффинг.
3 части окна.

1 - input (ввод символов)
2 - test (показываем инфу о том каким образом был сформирован пакет)
Строение пакета:
flag
source (1 byte)
destination (1 byte)
data (19 bytes)
check_sum (1 byte)
3 - output (вывод - совпадает с вводом)
_______________________________________________________________________

Лаба 3. Проверить целостность пакетов в сетях.
3 окна. Предусмотреть возможность внесения ошибки (искажения бита).

1: Ввод
Вводим последовательно символы, разбиваются на пакеты длинной 19 байт (полезные данные). Подсчет контрольного кода.

2: Тест
Ввывод полезных байт, в закодированном виде, контрольный код.

3: Вывод
Раскодлированные данные.

Код Хеминга, CRC (можно взять любой) + решение задачи на бумаге для сдачи.
Реализованы не ускоренные методы деления, а обычные.
Обнаружение одиночной ошибки, но не исправлять.
! Посмотреть поля Голуа.
_______________________________________________________________________

Лаба 4. Реализовать три формы адресации.
1.Каждая станция должна иметь возможность присваивать адрес: unicast, multicast, broadcast. + адрес назначения.
2.Адрес источника и назначения (каждый по одному байту).
3.Иерархическая форма адресации.
