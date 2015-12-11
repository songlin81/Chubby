/*
 * Created by SharpDevelop.
 * User: v0cn140
 * Date: 1/8/2015
 * Time: 8:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TransferDepositAccount
{
	class Program
	{
		public static void Main(string[] args)
		{	
			DepositAccount da=new DepositAccount("Bloom001", 0);
			da.AccountBalance=2000;
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
	
	interface Transfer{
		event EventHandler PerformTransfer;
	}
	
	class DepositAccount : Transfer{
	
		private event EventHandler _PerformTransfer;
		public event EventHandler PerformTransfer{
			add{ _PerformTransfer+=value; }
			remove{ _PerformTransfer-=value; }
		}

		public string AccountName {get; set;}
		
		private int accountBalance;
		public int AccountBalance {
			get {return accountBalance;}
			set { 
					accountBalance = value;
					_PerformTransfer(this, null);	//_PerformTransfer.Invoke(this, null);
				}
		}
		
		public DepositAccount(string accountName, int accountBalance){
			AccountName=accountName;
			this.accountBalance=accountBalance;
			this._PerformTransfer+=new EventHandler(LogTransfer);
		}
		
		public virtual void LogTransfer(object sender, EventArgs e)
		{
			Console.WriteLine("{0} is transferring ${1}", AccountName, AccountBalance);
		}
	}
}