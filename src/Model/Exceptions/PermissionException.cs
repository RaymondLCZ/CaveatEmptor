using System;

namespace Zee.Sample.CaveatEmptor.Model.Exceptions
{
	/// <summary> This exception is used to mark access violations. </summary>
	[Serializable]
	public class PermissionException : System.SystemException
	{
        //Expose a number of handy public constructors, 
        //that simply delegate to the base class.
		public PermissionException(){}
		public PermissionException(string message) : base(message){}
		public PermissionException(string message, Exception cause) : base(message, cause){}
		public PermissionException(Exception cause) : base("Access violation", cause){}
	}
}