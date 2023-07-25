# crawler-protect
A TagHelper to hide sensitive contents (like email addresses) in order to keep them from being accessed by malicious bots.

Inspired from [cloudflare email address obfuscation](https://developers.cloudflare.com/support/more-dashboard-apps/cloudflare-scrape-shield/what-is-email-address-obfuscation/)

## Example :
the razor : 
```html
    <p><protected placeholder="[Protected string]" class="protected-str badge text-bg-primary">My sensitive data</protected></p>
    <p><protected placeholder="[Protected email]" class="protected-lnk">test@example.com</protected></p>
    <p><protected href="/Test" class="protected-lnk">test@example.com</protected></p>
```
is interpreted to :
```html
    <p><a class="protected-str badge text-bg-primary" data-protected="226$af9bc291878c918b968b9487c286839683">[Protected string]</a></p>
    <p><a class="protected-lnk" data-protected="28$68796f685c79647d716c7079327f7371">[Protected email]</a></p>
    <p><a href="/Test" class="protected-lnk" data-protected="58$4e5f494e7a5f425b574a565f14595557">[Protected]</a></p>
```
and JS translate to :
```html
    <p><span class="protected-str badge text-bg-primary">My sensitive data</span></p>
    <p><a class="protected-lnk" href="mailto:test@example.com">test@example.com</a></p>
    <p><a href="mailto:test@example.com" class="protected-lnk">test@example.com</a></p>
```

## Usage
Tag : `<protected placeholder="String" href="/path" class="class-used-by-js">Content to protect</protected>`
Attributs :
- placeholder : (Optional) String placed in place of the content to be encoded. Default : "[Protected]"; 
- href : (Optional) Path to an explanation page. Default: Empty.
Other attributes are kept unchanged.

## Explanation page example
```
Sensitive content protection

You are unable to access this information.
Sensitive contents (like email addresses) on that page have been hidden in order to keep them from being accessed by malicious bots.
You must enable Javascript in your browser in order to decode this content.
```

## Setup
1. Create `ProtectedTagHelper` on your project (copy from file in this repository) and ajust the namespace;
2. Add `@addTagHelper TestApp.TagHelpers.ProtectedTagHelper, TestApp` in `_ViewImports.cshtml` (don't forget to adjust the namespace);
3. Add and import the `decode.js` to your protect;
4. You can use the helper.

## Disclaimer
This feature is obfuctation not encryption. It is not intended to protect data from hackers, just to prevent emails and datas from being captured by automatic crawlers.
