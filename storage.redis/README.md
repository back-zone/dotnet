#### back.zone's

### REDIS

#### Configuration object

The only required parameter is `end_points`. All other parameters are optional

```json
{
  "redis": {
    "end_points": [
      "localhost:6379"
    ],
    "command_name": "default",
    "connect_retry": 3,
    "allow_admin": false,
    "user": null,
    "password": null,
    "abort_on_connect_fail": false
  }
}
```
