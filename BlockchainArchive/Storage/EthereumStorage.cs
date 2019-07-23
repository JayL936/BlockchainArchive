using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainArchive.Storage
{
    public class EthereumStorage : IEthereumStorage
    {
        private string _senderAddress;
        private string _password;
        private string _abi = @"[{""constant"":false,""inputs"":[{""name"":""hash"",""type"":""string""},{""name"":""guid"",""type"":""string""}],""name"":""saveDocumentHash"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""guid"",""type"":""string""}],""name"":""getDocumentHash"",""outputs"":[{""name"":""ref"",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}]";
        private string _contractByteCode = "608060405234801561001057600080fd5b506104c2806100206000396000f3fe608060405234801561001057600080fd5b50600436106100365760003560e01c8063a82174cd1461003b578063bb6ccd941461016a575b600080fd5b6101686004803603604081101561005157600080fd5b81019060208101813564010000000081111561006c57600080fd5b82018360208201111561007e57600080fd5b803590602001918460018302840111640100000000831117156100a057600080fd5b91908080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525092959493602081019350359150506401000000008111156100f357600080fd5b82018360208201111561010557600080fd5b8035906020019184600183028401116401000000008311171561012757600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250929550610285945050505050565b005b6102106004803603602081101561018057600080fd5b81019060208101813564010000000081111561019b57600080fd5b8201836020820111156101ad57600080fd5b803590602001918460018302840111640100000000831117156101cf57600080fd5b91908080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152509295506102fe945050505050565b6040805160208082528351818301528351919283929083019185019080838360005b8381101561024a578181015183820152602001610232565b50505050905090810190601f1680156102775780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b816000826040518082805190602001908083835b602083106102b85780518252601f199092019160209182019101610299565b51815160209384036101000a600019018019909216911617905292019485525060405193849003810190932084516102f995919491909101925090506103f2565b505050565b60606000826040518082805190602001908083835b602083106103325780518252601f199092019160209182019101610313565b518151600019602094850361010090810a820192831692199390931691909117909252949092019687526040805197889003820188208054601f60026001831615909802909501169590950492830182900482028801820190528187529294509250508301828280156103e65780601f106103bb576101008083540402835291602001916103e6565b820191906000526020600020905b8154815290600101906020018083116103c957829003601f168201915b50505050509050919050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061043357805160ff1916838001178555610460565b82800160010185558215610460579182015b82811115610460578251825591602001919060010190610445565b5061046c929150610470565b5090565b61048a91905b8082111561046c5760008155600101610476565b9056fea265627a7a72305820aa8f7f7972ac8b6d2e0c5d6e6bff5a25be043f6403282e2dab9834ba3ee396d964736f6c634300050a0032";
        private Web3Geth _web3;

        public EthereumStorage(string senderAddress, string password)
        {
            _senderAddress = senderAddress;
            _password = password;

            _web3 = new Web3Geth();
        }

        public async Task<bool> SendDocumentHashToChain(string hash, string guid)
        {
            var unlockResult = await _web3.Personal.UnlockAccount.SendRequestAsync(_senderAddress, _password, 120);
            var transactionHash = await _web3.Eth.DeployContract.SendRequestAsync(_abi, _contractByteCode, _senderAddress, new HexBigInteger(900000));
            var receipt = await MineAndGetReceiptAsync(transactionHash);

            var contractAddress = receipt.ContractAddress;

            var contract = _web3.Eth.GetContract(_abi, contractAddress);

            var saveDocumentFunction = contract.GetFunction("saveDocumentHash");
            var getDocumentFunction = contract.GetFunction("getDocumentHash");

            transactionHash = await saveDocumentFunction.SendTransactionAsync(_senderAddress, new HexBigInteger(900000), null, hash, guid);

            receipt = await MineAndGetReceiptAsync(transactionHash);

            var result = await getDocumentFunction.CallAsync<string>(guid);

            return result == hash;
        }

        private async Task<TransactionReceipt> MineAndGetReceiptAsync(string transactionHash)
        {

            var miningResult = await _web3.Miner.Start.SendRequestAsync(6);
            //if (!miningResult)
            //    throw new Exception("Mining start failed.");

            var receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            miningResult = await _web3.Miner.Stop.SendRequestAsync();
            //if (!miningResult)
            //    throw new Exception("Mining stop failed.");

            return receipt;
        }
    }
}
