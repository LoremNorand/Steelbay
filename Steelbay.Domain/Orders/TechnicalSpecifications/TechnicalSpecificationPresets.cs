namespace Steelbay.Domain.Orders.TechnicalSpecifications;

using TSV = TechnicalSpecificationValue;


public static class TechnicalSpecificationPresets
{
    #region PUBLIC PROPERTIES

    public static Dictionary<string, TechnicalSpecificationValue> Fence => new()
    {
        { "Высота секции, м", TSV.Set(value: 2.5d) },
        { "Ширина секции (шаг столбов)", TSV.Set(value: 3.0d) },
        { "Тип фундамента столбов", TSV.Set(value: "Точечное бетонирование") },
        { "Подготовка под автоматику ворот", TSV.Set(value: true) },
        { "Тип защитного покрытия", TSV.Set(value: "Полимерно-порошковое") }
    };

    public static Dictionary<string, TechnicalSpecificationValue> Hangar => new()
    {
        { "Ширина пролёта, м", TSV.Set(value: 18d) },
        { "Шаг колонн, м", TSV.Set(value: 6d) },
        { "Высота, м", TSV.Set(value: 7.5d) },
        { "Снеговой регион", TSV.Set(value: 3) },
        { "Ветровой регион", TSV.Set(value: 2) },
        { "Утеплённое исполнение", TSV.Set(value: true) },
        { "Степень огнестойкости", TSV.Set(value: "IV Обычная") },
        { "Расчетная нагрузка на пол, т/м²", TSV.Set(value: 5d) }
    };

    public static Dictionary<string, TechnicalSpecificationValue> ProfileSheet => new()
    {
        { "Марка профиля", TSV.Set(value: "НС-35") },
        { "Рабочая ширина листа, мм", TSV.Set(value: 1000) },
        { "Длина реза (длина листа), м", TSV.Set(value: 6d) },
        { "Плотность цинкования, г/м²", TSV.Set(value: 140) }
    };

    public static Dictionary<string, TechnicalSpecificationValue> Roof => new()
    {
        { "Угол наклона кровли, градусы", TSV.Set(value: 15) },
        { "Тип кровельной системы", TSV.Set(value: "Двускатная") },
        { "Антиконденсатное покрытие", TSV.Set(value: false) },
        { "Наличие водосточной системы", TSV.Set(value: true) },
        { "Количество рядов снегозадержателей", TSV.Set(value: 1) }
    };

    public static Dictionary<string, TechnicalSpecificationValue> SandwichPanel => new()
    {
        { "Тип утеплителя", TSV.Set(value: "PIR (Пенополиизоцианурат)") },
        { "Толщина панели, мм", TSV.Set(value: 100) },
        { "Толщина внешнего листа металла, мм", TSV.Set(value: 0.5d) },
        { "Цвет внешней стороны (RAL)", TSV.Set(value: "RAL 9003") },
        { "Цвет внутренней стороны (RAL)", TSV.Set(value: "RAL 9002") },
        { "Тип профилирования", TSV.Set(value: "Микроволна") }
    };

    #endregion
}
