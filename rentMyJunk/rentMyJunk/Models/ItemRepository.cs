using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using rentMyJunk.Providers;

namespace rentMyJunk.Models
{
    public class ItemRepository : DocumentDb
    {
        //each repo can specify it's own database and document collection
        public ItemRepository() : base("rentanything", "Item") { }

        //Gets a list of all pieces of junk.
        public Task<List<Item>> GetItemsAsync()
        {
            return Task<List<Item>>.Run(() =>
                Client.CreateDocumentQuery<Item>(Collection.DocumentsLink)
                .ToList());
        }

        //Gets a piece of junk by its id.
        public Task<Item> GetItemAsync(string id)
        {
            return Task<Item>.Run(() =>
                Client.CreateDocumentQuery<Item>(Collection.DocumentsLink)
                .Where(i => i.id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        //Saves a new piece of junk
        public Task<ResourceResponse<Document>> SaveItem(Item item, Stream image)
        {
            BlobStorage blob = new BlobStorage();

            //Create the new guid for the item.
            item.id = new System.Guid().ToString();

            //Save the blob stream with the guid and update the imageuri.
            item.imageUri = blob.SaveBlob(item.id, image);
            
            //save the item to docuemntdb.
            return Client.CreateDocumentAsync(Collection.DocumentsLink, item);
        }

        //Updates a piece of junk.
        public Task<ResourceResponse<Document>> UpdateItemAsync(Item item)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == item.id)
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, item);
        }

        //Deletes a piece of junk by its id.
        public Task<ResourceResponse<Document>> DeleteItemAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }

        //Returns a list of junk for a given category name.
        public Task<List<Item>> GetItemsByCategory(string category)
        {
            return Task.Run(() =>
                Client.CreateDocumentQuery<Item>(Collection.DocumentsLink)
                .Where(i => i.category == category)
                .ToList());
        }

    }
}