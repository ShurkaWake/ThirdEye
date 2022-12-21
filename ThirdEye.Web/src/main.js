import { createApp } from 'vue'    
import Vuelidate from 'vuelidate'
import App from '@/App.vue'
import components from "@/components/UI"
import router from '@/router/router';
import 'materialize-css';

const app = createApp(App)

components.forEach(element => {
     app.component(element.name, element)
});

app
    .use(Vuelidate)
    .use(router)
    .mount('#app')
