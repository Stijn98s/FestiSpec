import { InspectorClient, AvailabilityClient, AuthClient, QuestionnaireClient, OpenAnswerClient, MultipleChoiceAnswerClient, ScaleAnswerClient, MeasureAnswerClient, EventClient, TableAnswerClient, PictureAnswerClient, DrawAnswerClient } from '@/services/ApiService'

const imageServerUrl = "https://festi-storage.azurewebsites.net/api/Images/Upload";

const baseUrl = "https://festiapi.azurewebsites.net";
// const baseUrl = 'http://localhost:63511';
const mobileUrl = "https://festims.azurewebsites.net";
// const mobileUrl = 'http://localhost:60423';
const authClient:AuthClient = new AuthClient(baseUrl);
const inspectorClient:InspectorClient = new InspectorClient(authClient);
const eventClient:EventClient = new EventClient(authClient);
const availabilityClient:AvailabilityClient = new AvailabilityClient(authClient);
const questionnaireClient:QuestionnaireClient = new QuestionnaireClient(authClient);
const openAnswerClient:OpenAnswerClient = new OpenAnswerClient(authClient);
const scaleAnswerClient:ScaleAnswerClient = new ScaleAnswerClient(authClient);
const measureAnswerClient:MeasureAnswerClient = new MeasureAnswerClient(authClient);
const drawAnswerClient:DrawAnswerClient = new DrawAnswerClient(authClient);
const pictureAnswerClient:PictureAnswerClient = new PictureAnswerClient(authClient);
const multipleChoiceAnswerClient:MultipleChoiceAnswerClient = new MultipleChoiceAnswerClient(authClient);
const tableAnswerClient:TableAnswerClient = new TableAnswerClient(authClient);

export {
  inspectorClient,
  eventClient,
  availabilityClient,
  questionnaireClient,
  openAnswerClient,
  scaleAnswerClient,
  measureAnswerClient,
  drawAnswerClient,
  pictureAnswerClient,
  multipleChoiceAnswerClient,
  tableAnswerClient,
  baseUrl,
  mobileUrl,
  imageServerUrl
}
