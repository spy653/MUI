using System;
using System.Threading;

namespace MUI
{
	public static class Input
	{
		private static uint selection;
		private static bool selected;

		public static uint Selection{ get => selection; set => selection = value; }
		public static bool Selected { get => selected; set => selected = value; }

		/// <summary> Processes user input for navigation. </summary>		
		public static void 	Handle(){
			if(Console.KeyAvailable)
			{
				ConsoleKey Key = Console.ReadKey(true).Key;
			
				switch(Key)
				{
					default: break;
					case ConsoleKey.UpArrow: 	
					case ConsoleKey.W: 			NavUp();	break;
					
					case ConsoleKey.DownArrow: 	
					case ConsoleKey.S: 			NavDown();	break;
					
					case ConsoleKey.LeftArrow: 	
					case ConsoleKey.A: 			
					case ConsoleKey.Backspace:	
					case ConsoleKey.D0:			NavBack();	break;

					case ConsoleKey.RightArrow: 
					case ConsoleKey.D: 			
					case ConsoleKey.Spacebar:	
					case ConsoleKey.Enter: 		NavEnter();	break;
		
					case ConsoleKey.Escape:		Core.Running = false; break;

					case ConsoleKey.D1:			Selection = 0; break;
					case ConsoleKey.D2:			Selection = 1; break;
					case ConsoleKey.D3:			Selection = 2; break;
					case ConsoleKey.D4:			Selection = 3; break;
					case ConsoleKey.D5:			Selection = 4; break;
					case ConsoleKey.D6:			Selection = 5; break;
					case ConsoleKey.D7:			Selection = 6; break;
					case ConsoleKey.D8:			Selection = 7; break;
					case ConsoleKey.D9:			Selection = 8; break;
				}
			}
			else
			{
				Thread.Sleep(100);
			}

			if(Selected && (Core.State.Options.Count>0))
			{
				try{
					Core.State.Options[(int)Selection%Core.State.Options.Count].Function();
				}catch (Exception e){
					TUI.Message($"Enter Selection Failed.\n{e}",3);
					Thread.Sleep(500);
				}
				Selected = false;
			}
		}

		/// <summary> Decrements selection counter. </summary>
		private static void NavUp(){ 
			if(Selection>0)
			{
				Selection--;
			}
		}
		
		/// <summary> Increments selection counter. </summary>
		private static void NavDown(){
			Selection++; 
		}
		
		/// <summary> Resets selection, and sets current state to root. </summary>
		private static void NavBack(){
			Selection= 0;
			Core.State = Core.Root;
		}
		
		/// <summary> Sets selected true. </summary>
		private static void NavEnter(){
			Selected = true; 
		}
	}
}