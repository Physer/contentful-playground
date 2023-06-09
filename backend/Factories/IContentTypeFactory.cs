﻿using Contentful.Core.Models;
using ContentfulManagement.ContentTypes;

namespace ContentfulManagement.Factories;

public interface IContentTypeFactory<T> where T : BaseType
{
    string ContentTypeId { get; }
    ContentType CreateContentType();
}
