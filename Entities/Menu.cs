using System;

namespace Entities
{
    public class Menu
    {
		int UserMenuChoose = -1;
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

			ConsoleKeyInfo userChoose = Console.ReadKey();

			if (char.IsDigit(userChoose.KeyChar))
			{
				UserMenuChoose = int.Parse(userChoose.KeyChar.ToString());
				if (UserMenuChoose < 0 || UserMenuChoose > 8)
				{
					ErrorMessage();

				}
				return UserMenuChoose;
			}
			else
			{
				ErrorMessage();
			}

			return UserMenuChoose;
		}
		public int СatalogMenu()
		{

			Console.WriteLine("\nСделайте выбор:\n");
			Console.WriteLine("1. Товары");
			Console.WriteLine("2. Категории товаров");
			Console.WriteLine("3. Единицы измерения");
			Console.WriteLine("0. Выход");

			ConsoleKeyInfo userChoose = Console.ReadKey();

			if (char.IsDigit(userChoose.KeyChar))
			{
				UserMenuChoose = int.Parse(userChoose.KeyChar.ToString());
				if (UserMenuChoose < 0 || UserMenuChoose > 3)
				{
					ErrorMessage();

				}
				return UserMenuChoose;
			}
			else
			{
				ErrorMessage();
			}

			return UserMenuChoose;
		}
		private void ErrorMessage()
		{
			Console.Clear();
			Console.WriteLine("Выберите число от 0 до 8.");
			AdminMenuI();
		}
	}
}
