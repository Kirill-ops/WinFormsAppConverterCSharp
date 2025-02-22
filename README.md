# Конвертер кода C#. Прототип

Приложение позволяет преобразовать классы (пока что только свойства классов) из выбранного пространства имён в интерфейсы языка ```TypeScript```.

Пример преобразования класса:
```
public class NameClass
{
  public string NameProperty { get; }
}
```
преобразуется в:
```
export interface INameClass
{
  nameProperty: string;
}
```
На данный момент можно преобразовать только группу классов из указанного пространства имён, все полученные интерфейсы сохраняются в файлах с расширение ```.ts```, на каждый интерфейс свой файл. Название файла образуется из названия интерфейса, например: 
```
 interface INameClass -> i-name-class.ts
```
Для группы интерфейсов создается файл ```index.ts```.
Пример содержания:
```
export type { INameClassOne } from './i-name-class-one.ts';
export type { INameClassTwo } from './i-name-class-two.ts';
export type { INameClassThree } from './i-name-class-three.ts';
```
Так же, если в интерфейсе используется другой(-ие) интерфейс(-ы), первому добавляется ```import```, пример:
```
import { IOtherClass } from ".";
export interface INameClass
{
  nameProperty: IOtherClass;
}
```

## Какие типы данных и во что могут конвертироваться
```
int,
long,
float,
double
decimal,   --> number
---------------------
bool       --> boolean
---------------------
DateTimeOffset,
DateOnly,
string,
Guid       --> string
---------------------
IReadOnlyList<NameClass> --> INameClass[]
---------------------
любой класс
NameClass  --> INameClass
```
