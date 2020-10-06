using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LawStorageApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlobsController : ControllerBase
    {
        // GET: api/<BlobsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BlobsController>/blob
        [HttpGet("{blobname}")]
        public async Task<ActionResult> Get([FromRoute]string blobName)
        {
            const string connectionString = "DefaultEndpointsProtocol=https;AccountName=lawpoc;AccountKey=ZdHGAtI53VpeoPx+ogmGLx7K/Egj/ppZJiyuMYNlPgBEEoC7tv+c9bebXumXysBD3piBx6LnE8GZQzEjhz81kQ==;EndpointSuffix=core.windows.net";
            const string blobContainerName = "testje";

            string containerEndpoint = $"https://lawpoc.blob.core.windows.net/{blobContainerName}";

            // Get a credential and create a client object for the blob container.
            BlobContainerClient containerClient = new BlobContainerClient(new Uri(containerEndpoint),
                                                                          new DefaultAzureCredential());
                                                                          //new DefaultAzureCredential(new DefaultAzureCredentialOptions {ManagedIdentityClientId = ""}));

            //var containerClient = new BlobContainerClient(connectionString, blobContainerName);
            BlobClient client = containerClient.GetBlobClient(blobName);

            Response<BlobDownloadInfo> blob = await client.DownloadAsync();
            return Ok(blob.Value.Content);
        }

        // POST api/<BlobsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BlobsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlobsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
