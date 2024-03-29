# H. Валидация карты (25 баллов)
> ограничение по времени на тест: **2.0 с**
> ограничение по памяти на тест: **512 мегабайт**
> ввод: **стандартный ввод**
> вывод: **стандартный вывод**

В этой задаче вам необходимо реализовать валидацию корректности карты для стратегической компьютерной игры.

Карта состоит из гексагонов (шестиугольников), каждый из которых принадлежит какому-то региону карты. В файлах игры карта представлена как **n**
строк по **m** символов в каждой (строки и символы в них нумеруются с единицы). Каждый нечетный символ каждой четной строки и каждый четный символ каждой нечетной строки — точка (символ **.** с ASCII кодом **46**); все остальные символы соответствуют гексагонам и являются заглавными буквами латинского алфавита. Буква указывает на то, какому региону принадлежит гексагон.

Посмотрите на картинку ниже, чтобы понять, как описание карты в файлах игры соответствует карте из шестиугольников.

![](https://espresso.codeforces.com/7328124ea7dceed3d5193303e8772c5d35c0558a.png)

*Соответствие описания карты в файле (слева) и самой карты (справа). Регионы R, G, V, Y и B окрашены в красный, зеленый, фиолетовый, желтый и синий цвет, соответственно.*

Вы должны проверить, что каждый регион карты является одной связной областью. Иными словами, не должно быть двух гексагонов, принадлежащих одному и тому же региону, которые не соединены другими гексагонами этого же региона.

![](https://espresso.codeforces.com/8897fdc34b5b4990c5b7fc4e1458c2ac87324b2a.png)

*Карта слева является корректной. Карта справа не является корректной, так как гексагоны, обозначенные цифрами **1** и **2**, принадлежат одному и тому же региону (обозначенному красным цветом), но не соединены другими гексагонами этого региона.*

Неполные решения этой задачи (например, недостаточно эффективные) могут быть оценены частичным баллом.
## Входные данные

В первой строке задано одно целое число **t** (**1≤t≤100**) — количество наборов входных данных.

Первая строка набора входных данных содержит два целых числа **n**
и **m** (**2≤n,m≤20**) — количество строк и количество символов в каждой строке в описании карты.

Далее следуют **n** строк по **m** символов в каждой — описание карты. Каждый нечетный символ каждой четной строки и каждый четный символ каждой нечетной строки — точка (символ **.** с ASCII кодом **46**); все остальные символы соответствуют гексагонам и являются заглавными буквами латинского алфавита.
## Выходные данные

На каждый набор входных данных выведите ответ в отдельной строке — `YES`, если каждый регион карты представляет связную область, или `NO`, если это не так.
### Пример
**Входные данные**

```
3
3 7
R.R.R.G
.Y.G.G.
B.Y.V.V
4 8
Y.R.B.B.
.R.R.B.V
B.R.B.R.
.B.B.R.R
2 7
G.B.R.G
.G.G.G.
```

**Выходные данные**

```
YES
NO
YES
```

### Примечание

Первые два набора входных данных из примера показаны на второй картинке в условии.