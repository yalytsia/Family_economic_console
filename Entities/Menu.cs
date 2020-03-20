using System;

namespace Entities
{
    public class Menu
    {
		public int AdminMenuI()
		{

			Console.WriteLine("\nСделайте выбор:\n");
			Console.WriteLine("1. Просмотр записей");
			Console.WriteLine("2. Добавить запись");
			Console.WriteLine("3. Удалить запись");
			Console.WriteLine("4. Искать запись");
			Console.WriteLine("5. Редактировать запись");
			Console.WriteLine("6. Сортировать записи");
			Console.WriteLine("7. Записать все данные в файл");
			Console.WriteLine("8. Изменить пароль");
			//Console.WriteLine("9. Возврат в главное меню" );
			Console.WriteLine("0. Выход");

			ConsoleKeyInfo iAdminChoose = Console.ReadKey(true);

			if (char.IsDigit(iAdminChoose.KeyChar))
			{
				var menuItem = int.Parse(iAdminChoose.KeyChar.ToString());
				if (menuItem < 0 || menuItem > 8)
				{
					ErrorMessage();
				}
				return menuItem;
			}
			else
			{
				ErrorMessage();
			}

			return -1;
		}

		private void ErrorMessage()
		{
			Console.Clear();
			Console.WriteLine("Выберите число от 0 до 8.");
			AdminMenuI();
		}
	}
}
