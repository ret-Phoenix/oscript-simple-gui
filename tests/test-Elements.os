#Использовать asserts

Перем юТест;

Процедура Инициализация()

КонецПроцедуры

Функция ПолучитьСписокТестов(Тестирование) Экспорт

	юТест = Тестирование;

	СписокТестов = Новый Массив;
	СписокТестов.Добавить("Тест_Должен_ВернутьКоличествоЭлементовНоль");
	СписокТестов.Добавить("Тест_Должен_ВернутьКоличествоЭлементовОдин");
	СписокТестов.Добавить("Тест_Должен_ВернутьСтрокуЭлементы");

	СписокТестов.Добавить("Тест_Должен_ВернутьЭлементФормы");
	СписокТестов.Добавить("Тест_Должен_ВернутьНеопределеноПриПоиске");

	СписокТестов.Добавить("Тест_Должен_УдалитьЭлемент");

	СписокТестов.Добавить("Тест_Должен_ПереместитьЭлементОдинРодитель");
	СписокТестов.Добавить("Тест_Должен_ПереместитьЭлементРазныеРодители");

	Возврат СписокТестов;

КонецФункции

Функция ПолучитьФорму()

	ПодключитьВнешнююКомпоненту("oscript-component/bin/Release/oscript-simple-gui.dll");
	ПростойГУИ = Новый ПростойГУИ();
	Возврат ПростойГУИ.СоздатьФорму();

КонецФункции

Процедура Тест_Должен_ВернутьКоличествоЭлементовНоль() Экспорт

	Форма = ПолучитьФорму();
	Форма.ПоказатьНеМодально();

	юТест.ПроверитьРавенство(Форма.Элементы.Количество(), 0);

КонецПроцедуры

Процедура Тест_Должен_ВернутьСтрокуЭлементы() Экспорт

	Форма = ПолучитьФорму();
	ЭлементыФормыСтр = Строка(Форма.Элементы);
	Форма.ПоказатьНеМодально();

	юТест.ПроверитьРавенство(ЭлементыФормыСтр, "Элементы");

КонецПроцедуры

Процедура Тест_Должен_ВернутьКоличествоЭлементовОдин() Экспорт

	Форма = ПолучитьФорму();
	ЭлементыФормы = Форма.Элементы;
	ЭлементыФормы.Добавить("ТекстовоеПоле1", "ПолеФормы", Неопределено);
	Форма.ПоказатьНеМодально();

	юТест.ПроверитьРавенство(Форма.Элементы.Количество(), 1);

КонецПроцедуры

Процедура Тест_Должен_ВернутьЭлементФормы() Экспорт

	Форма = ПолучитьФорму();
	ЭлементыФормы = Форма.Элементы;
	ЭлементыФормы.Добавить("ТекстовоеПоле1", "ПолеФормы", Неопределено);
	НайденныйЭлемент = ЭлементыФормы.Найти("ТекстовоеПоле1");
	Форма.ПоказатьНеМодально();

	юТест.ПроверитьНеРавенство(НайденныйЭлемент, Неопределено);

КонецПроцедуры

Процедура Тест_Должен_ВернутьНеопределеноПриПоиске() Экспорт

	Форма = ПолучитьФорму();
	ЭлементыФормы = Форма.Элементы;
	ЭлементыФормы.Добавить("ТекстовоеПоле1", "ПолеФормы", Неопределено);
	НайденныйЭлемент = ЭлементыФормы.Найти("ОшибочноеИмя");
	Форма.ПоказатьНеМодально();

	юТест.ПроверитьРавенство(НайденныйЭлемент, Неопределено);

КонецПроцедуры

Процедура Тест_Должен_УдалитьЭлемент() Экспорт

	Форма = ПолучитьФорму();
	ЭлементыФормы = Форма.Элементы;
	Элемент = ЭлементыФормы.Добавить("ТекстовоеПоле1", "ПолеФормы", Неопределено);
	Элемент.Заголовок = "ТекстовоеПоле1";
	НайденныйЭлемент = ЭлементыФормы.Найти("ТекстовоеПоле1");
	ЭлементыФормы.Удалить(НайденныйЭлемент);
	Форма.ПоказатьНеМодально();

КонецПроцедуры // ИмяПроцедуры()

Процедура Тест_Должен_ПереместитьЭлементОдинРодитель() Экспорт

	Форма = ПолучитьФорму();
	ЭлементыФормы = Форма.Элементы;

	Элемент1 = ЭлементыФормы.Добавить("ТекстовоеПоле1", "ПолеФормы", Неопределено);
	Элемент1.Заголовок = "ТекстовоеПоле1";

	Элемент2 = ЭлементыФормы.Добавить("ТекстовоеПоле2", "ПолеФормы", Неопределено);
	Элемент2.Заголовок = "ТекстовоеПоле2";

	Сообщить("Родитель: " + Элемент1.Родитель);

	ЭлементыФормы.Переместить(Элемент1,Элемент1.Родитель,Элемент2);
	
	Форма.ПоказатьНеМодально();

КонецПроцедуры

Процедура Тест_Должен_ПереместитьЭлементРазныеРодители() Экспорт

	Форма = ПолучитьФорму();
	ЭлементыФормы = Форма.Элементы;
	ВидГруппыФормы = Форма.ВидГруппыФормы;
	ВидПоляФормы = Форма.ВидПоляФормы;

	Группа1 = ЭлементыФормы.Добавить("ОбычнаяГруппа1", "ГруппаФормы", Неопределено);
	Группа1.Вид = ВидГруппыФормы.ОбычнаяГруппа;
	Группа1.Заголовок = "Группа1";

	Элемент1 = ЭлементыФормы.Добавить("ТекстовоеПоле1", "ПолеФормы", Группа1);
	Элемент1.Вид = ВидПоляФормы.ПолеВвода;
	Элемент1.Заголовок = "ТекстовоеПоле1";

	Группа2 = ЭлементыФормы.Добавить("ОбычнаяГруппа2", "ГруппаФормы", Неопределено);
	Группа2.Вид = ВидГруппыФормы.ОбычнаяГруппа;
	Группа2.Заголовок = "Группа2";

	Элемент2 = ЭлементыФормы.Добавить("ТекстовоеПоле2", "ПолеФормы", Группа2);
	Элемент2.Вид = ВидПоляФормы.ПолеВвода;
	Элемент2.Заголовок = "ТекстовоеПоле2";

	Элемент3 = ЭлементыФормы.Добавить("ТекстовоеПоле3", "ПолеФормы", Группа2);
	Элемент3.Вид = ВидПоляФормы.ПолеВвода;
	Элемент3.Заголовок = "ТекстовоеПоле3";

	Кнопка1 = ЭлементыФормы.Добавить("Кнопка1", "КнопкаФормы", Группа2);
	Кнопка1.Заголовок = "Кнопка1";

	Группа3 = ЭлементыФормы.Добавить("ОбычнаяГруппа3", "ГруппаФормы", Неопределено);
	Группа3.Вид = ВидГруппыФормы.ОбычнаяГруппа;
	Группа3.Заголовок = "Группа3";

	ЭлементыФормы.Переместить(Элемент1, Элемент3.Родитель, Элемент3);
	ЭлементыФормы.Переместить(Кнопка1, Элемент3.Родитель, Элемент1);
	ЭлементыФормы.Переместить(Группа3, Группа1, Неопределено);
	ЭлементыФормы.Переместить(Элемент1, Группа3, Неопределено);
	ЭлементыФормы.Переместить(Кнопка1, Группа1.Родитель, Неопределено);
	
	Форма.ПоказатьНеМодально();
	// Форма.Показать();

КонецПроцедуры





//////////////////////////////////////////////////////////////////////////////////////
// Инициализация

Инициализация();