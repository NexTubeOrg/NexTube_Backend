![Logo](https://nextube.itstep.click/assets/logo-8d85db41.svg)


# NexTube Backend

An open-source social media, where registered users can upload their awesome videos, without censure and limitations.
## Demo

Deployed in AWS instance: 
https://nextube.itstep.click/


## Installation

Install and run daemon your own instance in docker

```bash
  git clone https://github.com/NexTubeOrg/NexTube_Backend.git
  cd ./NexTube_Backend
  docker build -t nextube .
  docker run -d --restart=always --name nextube_container -p 5453:80 nextube
```
    
