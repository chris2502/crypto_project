import { DevisFrontPage } from './app.po';

describe('devis-front App', () => {
  let page: DevisFrontPage;

  beforeEach(() => {
    page = new DevisFrontPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
