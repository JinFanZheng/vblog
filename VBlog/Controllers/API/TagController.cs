﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VBlog.Services.Interfaces;
using VBlog.Services.Messages;
using VBlog.Services.Messages.Models;
using VBlog.Services.Messages.Requests;
using VBlog.Services.Messages.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VBlog.Controllers
{
    public class TagController : ApiControllerBase
    {

        private readonly ITagService _service;
        public TagController(ITagService service)
        {
            _service = service;
        }
        [HttpPost("insert")]
        public async Task<APIResult<SaveResponse>> Insert([FromBody]TagModel model) => await _service.InsertAsync(model);
        [HttpGet("delete")]
        public async Task<APIResult<string>> Delete(int id) => await _service.DeleteAsync(id);
        [HttpGet("getall")]
        public async Task<APIResult<List<TagModel>>> GetAll() => await _service.GetAllAsync();

    }
}
