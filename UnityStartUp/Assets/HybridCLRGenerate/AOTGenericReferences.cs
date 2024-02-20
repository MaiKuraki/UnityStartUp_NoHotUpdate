using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"MessagePipe.Zenject.dll",
		"UnityEngine.CoreModule.dll",
		"Zenject.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// System.Action<object>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<object>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<object>
	// System.Func<byte>
	// System.Func<object,object,object>
	// System.Predicate<object>
	// Zenject.DiContainer.<>c__DisplayClass206_0<object>
	// }}

	public void RefMethods()
	{
		// Zenject.DiContainer MessagePipe.DiContainerExtensions.BindMessageBroker<object>(Zenject.DiContainer,MessagePipe.MessagePipeOptions)
		// object UnityEngine.Object.FindObjectOfType<object>()
		// Zenject.IdScopeConcreteIdArgConditionCopyNonLazyBinder Zenject.DiContainer.BindInstance<object>(object)
		// Zenject.FromBinderNonGeneric Zenject.DiContainer.BindInterfacesTo<object>()
	}
}