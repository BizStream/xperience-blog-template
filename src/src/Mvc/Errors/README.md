# `BlogTemplate.Mvc.Errors`

## Features

- Exception Handler
- [Xperience] StatusCodePages
- Error Page

## Notes

- Error pages are based on [`BizStream/xperience-status-code-pages`](https://github.com/BizStream/xperience-status-code-pages).
- Execption Handler simply returns `HTTP 500`, resulting in the `StatusCodePages` middleware to return the `500` error page.

