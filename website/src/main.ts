import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { NestExpressApplication } from '@nestjs/platform-express';
import { join } from 'path';
import * as hbs from 'hbs';
import { ConfigService } from '@nestjs/config';

async function bootstrap() {
  const app = await NestFactory.create<NestExpressApplication>(AppModule);
  app.setBaseViewsDir(join(__dirname, '..', 'views'));

  hbs.registerPartials(join(__dirname, '..', 'views', 'partials'));
  app.useStaticAssets(join(__dirname, '..', 'public'));
  app.setViewEngine('hbs');

  const configService = new ConfigService();
  //await app.listen(configService.getOrThrow('PORT'));
  await app.listen(80);
}
bootstrap();
