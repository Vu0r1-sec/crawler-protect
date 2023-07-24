# crawler-protect
A few lines of code to limit the extraction of your emails and other sensitive strings from your website by crawlers.

Inpired from https://developers.cloudflare.com/support/more-dashboard-apps/cloudflare-scrape-shield/what-is-email-address-obfuscation/

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

## Disclaimer
This feature is obfuctation not encryption. It is not intended to protect data from hackers, just to prevent emails and datas from being captured by automatic crawlers.
