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
			Console.WriteLine("3. Удалить запись");
			Console.WriteLine("4. Просмотр записей");
			Console.WriteLine("5. Редактирование записей");
			Console.WriteLine("0. Выход");

			ConsoleKeyInfo userChoose = Console.ReadKey();

			if (char.IsDigit(userChoose.KeyChar))
			{
				UserMenuChoose = int.Parse(userChoose.KeyChar.ToString());
				if (UserMenuChoose < 0 || UserMenuChoose > 5)
				{
					Console.Clear();
					ErrorMessage(0, 5);
					AdminMenuI();
				}
				return UserMenuChoose;
			}
			else
			{
				Console.Clear();
				ErrorMessage(0, 5);
				AdminMenuI();
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
					ErrorMessage(0, 3);

				}
				return UserMenuChoose;
			}
			else
			{
				ErrorMessage(0, 3);
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
					ErrorMessage(0, 4);

				}
				return UserMenuChoose;
			}
			else
			{
				ErrorMessage(0, 4);
			}

			return UserMenuChoose;
		}
		private void ErrorMessage(int from, int to)
		{
			//Console.Clear();
			Console.WriteLine($"Выберите число от {from} до {to}.");

		}
		public int AskAddRecord()
		{
		
			Console.WriteLine("1. Да, добавить запись в справочник и покупуку.");
			Console.WriteLine("2. Нет.");
			ConsoleKeyInfo userChoose = Console.ReadKey();
			if (char.IsDigit(userChoose.KeyChar))
			{
				UserMenuChoose = int.Parse(userChoose.KeyChar.ToString());
				if (UserMenuChoose < 1 || UserMenuChoose > 2)
				{
					ErrorMessage(1, 2);

				}
				return UserMenuChoose;
			}
			else
			{
				ErrorMessage(1, 2);
			}
			return UserMenuChoose;
		}
		public int DeleteMenu()
		{
			Console.Clear();
			Console.WriteLine("\nУдалить:\n");
			Console.WriteLine("1. Товар");
			Console.WriteLine("2. Категорию товаров");
			Console.WriteLine("3. Единицу измерения");
			Console.WriteLine("4. Покупку");
			Console.WriteLine("0. Выход в предыдущее меню");

			ConsoleKeyInfo userChoose = Console.ReadKey();

			if (char.IsDigit(userChoose.KeyChar))
			{
				UserMenuChoose = int.Parse(userChoose.KeyChar.ToString());
				if (UserMenuChoose < 0 || UserMenuChoose > 4)
				{
					ErrorMessage(0, 4);

				}
				return UserMenuChoose;
			}
			else
			{
				ErrorMessage(0, 4);
			}

			return UserMenuChoose;
		}
		public int EditMenu()
		{
			Console.Clear();
			Console.WriteLine("\nРедактировать:\n");
			Console.WriteLine("1. Товар");
			Console.WriteLine("2. Категорию товаров");
			Console.WriteLine("3. Единицу измерения");
			Console.WriteLine("0. Выход в предыдущее меню");

			ConsoleKeyInfo userChoose = Console.ReadKey();

			if (char.IsDigit(userChoose.KeyChar))
			{
				UserMenuChoose = int.Parse(userChoose.KeyChar.ToString());
				if (UserMenuChoose < 0 || UserMenuChoose > 3)
				{
					ErrorMessage(0, 3);

				}
				return UserMenuChoose;
			}
			else
			{
				ErrorMessage(0, 3);
			}

			return UserMenuChoose;
		}

	}
}
