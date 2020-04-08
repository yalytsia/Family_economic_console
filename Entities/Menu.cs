using System;

namespace Entities
{
    public class Menu
    {
		int UserMenuChoose = -1;
		public int AdminMenuI()
		{
			Console.Clear();
			Console.WriteLine("\nСделайте выбор:\n");
			Console.WriteLine("1. Добавить покупку" );
			Console.WriteLine("2. Добавить запись в справочник");
			Console.WriteLine("3. Редактировать запись");
			Console.WriteLine("4. Удалить запись");
			Console.WriteLine("5. Просмотр записей");
			Console.WriteLine("6. Статистика");
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
		public int СatalogsMenu()
		{
			Console.Clear();
			Console.WriteLine("\nДобавить в:\n");
			Console.WriteLine("1. Товары");
			Console.WriteLine("2. Категории товаров");
			Console.WriteLine("3. Единицы измерения");
			Console.WriteLine("0. Выход в предыдущее меню");

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
		public int ViewRecordMenu()
		{
			Console.Clear();
			Console.WriteLine("\n Просмотр списка:\n");
			Console.WriteLine("1. Товары");
			Console.WriteLine("2. Категории товаров");
			Console.WriteLine("3. Единицы измерения");
			Console.WriteLine("4. Покупки");
			Console.WriteLine("0. Выход в предыдущее меню");

			ConsoleKeyInfo userChoose = Console.ReadKey();

			if (char.IsDigit(userChoose.KeyChar))
			{
				UserMenuChoose = int.Parse(userChoose.KeyChar.ToString());
				if (UserMenuChoose < 0 || UserMenuChoose > 4)
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
