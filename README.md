# AdviceSlipService

Sample web api that replicates Advice Slip JSON API

## Requirements

Implement an application that provides a list of phrases related to a topic it inserts.

The application provides a REST API method called **GiveMeAdvice**.

The method shall accept the following parameters:
- **topic**, required, a string containing the topic
- **amount**, optional, an integer indicating the maximum amount of phrases to return

The response must be a list of advices related to the given parameter, each represented
by a string.

The Advice Slip JSON public API (https://api.adviceslip.com/) should be used as the data
source.

More infos in the pdf doc [Software Engineer Assignment](/Software Engineer Assignment .pdf)


