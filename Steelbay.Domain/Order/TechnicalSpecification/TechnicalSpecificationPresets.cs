namespace Steelbay.Domain.Order.TechnicalSpecification;

using TSV = TechnicalSpecificationValue;


public static class TechnicalSpecificationPresets
{
    #region PUBLIC PROPERTIES

    public static Dictionary<string, TechnicalSpecificationValue> Fence => new()
    {
        { "Высота секции, м", TSV.Set(2.5d) },
        { "Ширина секции (шаг столбов)", TSV.Set(3.0d) },
        { "Тип фундамента столбов", TSV.Set("Точечное бетонирование") },
        { "Подготовка под автоматику ворот", TSV.Set(true) },
        { "Тип защитного покрытия", TSV.Set("Полимерно-порошковое") }
    };

    public static Dictionary<string, TechnicalSpecificationValue> Hangar => new()
    {
        { "Ширина пролёта, м", TSV.Set(18d) },
        { "Шаг колонн, м", TSV.Set(6d) },
        { "Высота, м", TSV.Set(7.5d) },
        { "Снеговой регион", TSV.Set(3) },
        { "Ветровой регион", TSV.Set(2) },
        { "Утеплённое исполнение", TSV.Set(true) },
        { "Степень огнестойкости", TSV.Set("IV Обычная") },
        { "Расчетная нагрузка на пол, т/м²", TSV.Set(5d) }
    };

    public static Dictionary<string, TechnicalSpecificationValue> ProfileSheet => new()
    {
        { "Марка профиля", TSV.Set("НС-35") },
        { "Рабочая ширина листа, мм", TSV.Set(1000) },
        { "Длина реза (длина листа), м", TSV.Set(6d) },
        { "Плотность цинкования, г/м²", TSV.Set(140) }
    };

    public static Dictionary<string, TechnicalSpecificationValue> Roof => new()
    {
        { "Угол наклона кровли, градусы", TSV.Set(15) },
        { "Тип кровельной системы", TSV.Set("Двускатная") },
        { "Антиконденсатное покрытие", TSV.Set(false) },
        { "Наличие водосточной системы", TSV.Set(true) },
        { "Количество рядов снегозадержателей", TSV.Set(1) }
    };

    public static Dictionary<string, TechnicalSpecificationValue> SandwichPanel => new()
    {
        { "Тип утеплителя", TSV.Set("PIR (Пенополиизоцианурат)") },
        { "Толщина панели, мм", TSV.Set(100) },
        { "Толщина внешнего листа металла, мм", TSV.Set(0.5d) },
        { "Цвет внешней стороны (RAL)", TSV.Set("RAL 9003") },
        { "Цвет внутренней стороны (RAL)", TSV.Set("RAL 9002") },
        { "Тип профилирования", TSV.Set("Микроволна") }
    };

    #endregion
}
