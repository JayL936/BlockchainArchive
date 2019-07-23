pragma solidity ^0.5.10;

contract BlockchainArchive {

    mapping (string => string) public documents;

    function saveDocumentHash(string memory hash, string memory guid) public {
        documents[guid] = hash;
    }

    function getDocumentHash(string memory guid) public view returns (string memory) {
        return documents[guid];
    }
}