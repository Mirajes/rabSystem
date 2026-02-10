using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class Test_UniTask: MonoBehaviour
{
	[SerializeField] private int _coins = 0;
	[SerializeField] private float _coinDelay = 3f;

	CancellationTokenSource _token;

	private void Start()
	{
		GameLoop().Forget();
	}

	public void Stop()
	{
		_token.Cancel();
		_token.Dispose();
	}

	private async UniTask AddCoin(CancellationToken token)
	{

		while (token.IsCancellationRequested)
		{
			_coins++;
			await UniTask.Delay(TimeSpan.FromSeconds(_coinDelay), cancellationToken: token); // token ne obyazatelen, mozhono ne zaparivatca
			// eto delaetca chtobi vse taski stopit' 
		}
	}

	// poka ne rabotaet
	private async UniTask GameLoop()
	{
        _token = new CancellationTokenSource();
        var token = _token.Token;

		AddCoin(token).Forget();
        await UniTask.WaitUntilCanceled(token); // vkluchaet v sebe drugoi UniTask i zhet poka ne zakonchitca

		print("MoneyFinised");
	}
}
