using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.StateMachines.Transitions;
using UnityUtils.AddressableUtils;
using Utils.StateMachines;

namespace UnityUtils.Systems.States
{
	public abstract class BehaviourState<TComponent, TKey> : State<TKey>
		where TComponent : MonoBehaviour
	{
		[SerializeField] private LazyAddressable<TComponent> viewAddressable;

		protected TComponent Instance { get; private set; }
		protected virtual bool DoReleaseAsset => true;

		protected override async Task OnPreload(IStateData<TKey> data)
		{
			if (!Instance)
			{
				Instance = await viewAddressable;
				Instance.transform.localPosition = Vector3.zero;
				if (Instance.transform is RectTransform rect)
					rect.sizeDelta = Vector3.zero;
			}

			if (Instance is IBehaviourStateTransition transition)
			{ 
				await transition.Preload(Instance);
			}
			else
			{
				Instance.gameObject.SetActive(false);
			}
		}

		protected override async Task OnEnter()
		{
			if (Instance is IBehaviourStateTransition transition)
			{	
				await transition.Enter(Instance); 
			}
			else
			{
				Instance.gameObject.SetActive(true);
			}

			await base.OnEnter();
		}

		protected override async Task OnExit()
		{
			if (Instance)
			{
				if (Instance is IBehaviourStateTransition transition)
				{
					await transition.Exit(Instance);
				}
				else
				{
					Instance.gameObject.SetActive(false);
				}
			}

			await base.OnExit();
		}

		protected override async Task OnCleanup()
		{
			if (DoReleaseAsset)
			{
				await viewAddressable.DisposeAsync();
			}

			await base.OnCleanup();
		}

		public override async Task Reload(IStateData<TKey> data)
		{
			//Do not use base reload to avoid disposing
			await OnPreload(data);
			await OnEnter();
		}
	}
}
